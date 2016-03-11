using Autofac;

namespace messaging
{
    public class MessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<MessageConsumerFactory>().AsSelf();
        }
    }
}
