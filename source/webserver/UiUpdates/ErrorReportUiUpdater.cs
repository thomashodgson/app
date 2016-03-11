using System;
using System.Collections.Generic;
using messaging;
using models;

namespace webserver.UiUpdates
{
    class ErrorReportUiUpdaterConfig : IUiUpdatorMessageConsumerConfig
    {
        public Event Event => Event.ErrorReported;
        public IEnumerable<string> RequestUrls => models.Urls.RequestUrls.AllUrls;
        public Func<MessageData, dynamic> ViewModelFragmentFactory => messageData => new
        {
            Error = messageData.Error
        };
    }
}

