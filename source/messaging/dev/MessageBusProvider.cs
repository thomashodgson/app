namespace messaging.dev
{
    class MessageBusProvider : IMessageBusProvider
    {
        private readonly MessageBus _messageBus;

        public MessageBusProvider()
        {
            _messageBus = new MessageBus();
        }

        public IMessageBus GetMessageBus()
        {
            return _messageBus;
        }
    }
}
