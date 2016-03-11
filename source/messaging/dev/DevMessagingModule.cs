using Autofac;

namespace messaging.dev
{
   public class DevMessagingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<MessageBusProvider>().As<IMessageBusProvider>().SingleInstance();
            builder.Register(ctx => ctx.Resolve<IMessageBusProvider>().GetMessageBus()).As<IMessageBus>();
            builder.RegisterModule<MessagingModule>();
        }
    }
}
