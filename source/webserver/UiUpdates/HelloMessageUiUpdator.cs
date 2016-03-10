using messaging;
using models;
using models.Urls;
using Serilog;

namespace webserver.UiUpdates
{
    class HelloMessageUiUpdator : UiUpdatingMessageConsumer
    {
        public HelloMessageUiUpdator(UiUpdator uiUpdator, IMessageBus messageBus, ILogger logger) : 
            base(uiUpdator, messageBus, logger, Event.HelloMessageCreated, RequestUrls.Hello)
        {
        }

        protected override dynamic GetViewModelFragment(MessageData messageData)
        {
            return new
            {
                HelloMessageViewModel = new
                {
                    Message = messageData.RespondedHelloMessage,
                    IsLoading = false
                }
            };
        }

    } class GoodbyeMessageUiUpdator : UiUpdatingMessageConsumer
    {
        public GoodbyeMessageUiUpdator(UiUpdator uiUpdator, IMessageBus messageBus, ILogger logger) : 
            base(uiUpdator, messageBus, logger, Event.GoodbyeMessageCreated, RequestUrls.Bye)
        {
        }

        protected override dynamic GetViewModelFragment(MessageData messageData)
        {
            return new
            {
                HelloMessageViewModel = new
                {
                    Message = messageData.RespondedHelloMessage,
                    IsLoading = false
                }
            };
        }
    }
}
