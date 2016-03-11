using Autofac;
using messaging;
using RabbitMq_messaging.messaging.rabbitMq;

namespace RabbitMq_messaging
{
    public class RabbitMqModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<MessageBusProvider>().As<IMessageBusProvider>();
            builder.Register(ctx => ctx.Resolve<IMessageBusProvider>().GetMessageBus()).As<IMessageBus>();
            builder.RegisterModule<MessagingModule>();
        }
    }
}
