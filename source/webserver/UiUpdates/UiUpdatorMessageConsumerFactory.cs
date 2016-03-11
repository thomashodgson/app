using System;
using messaging;
using Serilog;

namespace webserver.UiUpdates
{
    class UiUpdatorMessageConsumerFactory
    {
        private readonly UiUpdator _uiUpdator;
        private readonly IMessageBus _messageBus;
        private readonly ILogger _logger;

        public UiUpdatorMessageConsumerFactory(IMessageBus messageBus, ILogger logger, UiUpdator uiUpdator)
        {
            _uiUpdator = uiUpdator;
            _messageBus = messageBus;
            _logger = logger;
        }

        public MessageConsumer Create(IUiUpdatorMessageConsumerConfig config)
        {
            Action<IMessageBus, Message> messageHandler = (_, message) =>
                _uiUpdator.SendViewModelFragmentToUser(message.User.Id, config.ViewModelFragmentFactory(message.Data));
            return new MessageConsumer(_messageBus, _logger, config.Event, config.RequestUrls, messageHandler);
        }
    }
}