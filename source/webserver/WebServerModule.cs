using Autofac;
using messaging;
using webserver.Auth;
using webserver.UiUpdates;

namespace webserver
{
    class WebServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<UiUpdator>().AsSelf();
            builder.RegisterType<AuthoriseAutomatically>().As<ICredentialAuthenticator>();
            builder.RegisterType<AuthoriseAutomatically>().As<ICredentialAuthenticator>();
            builder.RegisterType<ErrorReportUiUpdater>().As<IMessageConsumer>();
            builder.RegisterType<HelloMessageUiUpdator>().As<IMessageConsumer>();
            builder.RegisterType<GoodbyeMessageUiUpdator>().As<IMessageConsumer>();
        }
    }
}
