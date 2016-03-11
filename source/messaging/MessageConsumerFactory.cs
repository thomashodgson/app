using Serilog;

namespace messaging
{
    public class MessageConsumerFactory
    {
        private readonly IMessageBus _messageBus;
        private readonly ILogger _logger;

        public MessageConsumerFactory(IMessageBus messageBus, ILogger logger)
        {
            _messageBus = messageBus;
            _logger = logger;
        }

        public MessageConsumer Create(IMessageConsumerConfig config)
        {
            return new MessageConsumer(_messageBus, _logger, config.Event, config.RequestUrls, config.MessageHandler);
        }
    }
}
