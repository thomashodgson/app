namespace messaging
{
    public interface IMessageBusProvider
    {
        IMessageBus GetMessageBus();
    }
}