using System;
using System.Collections.Generic;
using messaging;
using models;

namespace webserver.UiUpdates
{
    class GoodbyeMessageUiUpdatorConfig : IUiUpdatorMessageConsumerConfig
    {
        public Event Event => Event.GoodbyeMessageCreated;
        public IEnumerable<string> RequestUrls => new[] { models.Urls.RequestUrls.Bye };
        public Func<MessageData, dynamic> ViewModelFragmentFactory => messageData => new
        {
            HelloMessageViewModel = new
            {
                Message = messageData.RespondedHelloMessage,
                IsLoading = false
            }
        };
    }
}