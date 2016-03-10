using Autofac;
using messaging;
using webserver.Auth;

namespace webserver
{
    class WebServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<UiUpdator>().As<IMessageConsumer>();
            builder.RegisterType<AuthoriseAutomatically>().As<ICredentialAuthenticator>();
        }
    }
}
