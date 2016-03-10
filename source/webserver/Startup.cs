using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using app_config;
using Autofac;
using Autofac.Integration.SignalR;
using Autofac.Integration.WebApi;
using messaging;
using messaging.dev;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.StaticFiles;
using Owin;
using RabbitMq_messaging;
using LoggerFactory = SerilogWeb.Owin.LoggerFactory;

[assembly: OwinStartup(typeof(webserver.Startup))]

namespace webserver
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            SetupRoutes(config);

            ConfigureAuth(appBuilder);
            
            var container = SetupAutofactWebapi(appBuilder, config);
            SetupFileServer(appBuilder);

            StartMessageConsumers(container);
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseMicrosoftAccountAuthentication(new MicrosoftAccountAuthenticationOptions
            {
                ClientId = "000000004C18160A",
                ClientSecret = "1Ty7nrv6ksJD5ZFayBmbxO3d9d8TqNN5", 
                CallbackPath = new PathString("/signin-microsoft"),
            });
        }

        private static bool IsApiRequest(IOwinRequest request)
        {
            return request.Uri.AbsolutePath.StartsWith("/api");
        }

        private void StartMessageConsumers(IContainer container)
        {
            foreach (var messageConsumer in container.Resolve<IEnumerable<IMessageConsumer>>())
            {
                messageConsumer.Start();
            }
        }

        private static void SetupFileServer(IAppBuilder appBuilder)
        {
            appBuilder.UseFileServer(new FileServerOptions()
            {
                RequestPath = PathString.Empty,
                FileSystem = new PhysicalFileSystem("webbuild")
            });
        }

        private static IContainer SetupAutofactWebapi(IAppBuilder appBuilder, HttpConfiguration config)
        {

            var hubConfig = new HubConfiguration()
            {
                EnableDetailedErrors = true
            };
            var builder = new ContainerBuilder();
            builder.RegisterHubs(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var appConfigProvider = new AppConfigProvider();
            builder.RegisterInstance(appConfigProvider).As<IConfigProvider>();
            builder.RegisterModule<LoggingModule>();
            builder.RegisterModule<WebServerModule>();
            
            RegisterMessaging(builder, appConfigProvider.GetConfig());

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            hubConfig.Resolver = new AutofacDependencyResolver(container);
            
            // allows me to get at the hubcontexts, if instead i use GlobalHost.ConnectionManage nothing gets sent to the client
            var newBuilder = new ContainerBuilder();
            newBuilder.RegisterInstance(hubConfig.Resolver.Resolve<IConnectionManager>());
            newBuilder.Update(container);
            //end

            appBuilder.UseWebApi(config);
            appBuilder.UseAutofacMiddleware(container);
            appBuilder.UseAutofacWebApi(config);
            appBuilder.MapSignalR(hubConfig);
            appBuilder.SetLoggerFactory(new LoggerFactory(container.Resolve<Serilog.ILogger>()));


            return container;
        }

        private static void RegisterMessaging(ContainerBuilder builder, Config config)
        {
            if (config.RabbitMqHostname != null)
            {
                builder.RegisterModule<RabbitMqModule>();
            }
            else
            {
                builder.RegisterModule<DevMessagingModule>();
            }

            builder.Register(ctx => ctx.Resolve<IMessageBusProvider>().GetMessageBus()).As<IMessageBus>();
        }

        private static void SetupRoutes(HttpConfiguration config)
        {

            config.MapHttpAttributeRoutes();
        }
    }
}
