using messaging;
using models;
using models.Urls;
using Serilog;

namespace webserver.UiUpdates
{
    class ErrorReportUiUpdater : UiUpdatingMessageConsumer
    {
        public ErrorReportUiUpdater(UiUpdator uiUpdator, IMessageBus messageBus, ILogger logger) :
            base(uiUpdator, messageBus, logger, Event.ErrorReported, RequestUrls.AllUrls)
        {
        }

        protected override dynamic GetViewModelFragment(MessageData messageData)
        {
            return new
            {
                Error = messageData.Error
            };
        }
    }
}

