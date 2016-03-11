using app_base_module;
using Autofac;
using webserver.Auth;
using webserver.UiUpdates;

namespace webserver
{
    class WebServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterModule<BaseModule>();
            builder.RegisterType<UiUpdator>().AsSelf();
            builder.RegisterType<AuthoriseAutomatically>().As<ICredentialAuthenticator>();
            builder.RegisterType<ErrorReportUiUpdaterConfig>().As<IUiUpdatorMessageConsumerConfig>();
            builder.RegisterType<HelloMessageUiUpdatorConfig>().As<IUiUpdatorMessageConsumerConfig>();
            builder.RegisterType<GoodbyeMessageUiUpdatorConfig>().As<IUiUpdatorMessageConsumerConfig>();
            builder.RegisterType<UiUpdatorMessageConsumerFactory>().AsSelf();
        }
    }
}
