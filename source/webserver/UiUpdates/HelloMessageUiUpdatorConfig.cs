using System;
using System.Collections.Generic;
using messaging;
using models;

namespace webserver.UiUpdates
{
    class HelloMessageUiUpdatorConfig : IUiUpdatorMessageConsumerConfig
    {
        public Event Event => Event.HelloMessageCreated;
        public IEnumerable<string> RequestUrls => new[] {models.Urls.RequestUrls.Hello};
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
