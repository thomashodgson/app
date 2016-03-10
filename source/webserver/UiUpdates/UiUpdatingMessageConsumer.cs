using System.Collections.Generic;
using messaging;
using models;
using Serilog;

namespace webserver.UiUpdates
{
    abstract class UiUpdatingMessageConsumer : MessageConsumer
    {
        private readonly UiUpdator _uiUpdator;

        protected UiUpdatingMessageConsumer(UiUpdator uiUpdator, IMessageBus messageBus, ILogger logger, Event @event, string requestUrl) :
            this(uiUpdator, messageBus, logger, @event, new[] {requestUrl})
        {
        }

        protected UiUpdatingMessageConsumer(UiUpdator uiUpdator, IMessageBus messageBus, ILogger logger, Event @event, IEnumerable<string> requestUrls) : 
            base(messageBus, logger, @event, requestUrls)
        {
            _uiUpdator = uiUpdator;
        }

        protected abstract dynamic GetViewModelFragment(MessageData messageData);

        protected override void OnMessage(Message message)
        {
            _uiUpdator.SendViewModelFragmentToUser(message.User.Id, GetViewModelFragment(message.Data));
        }
    }
}
