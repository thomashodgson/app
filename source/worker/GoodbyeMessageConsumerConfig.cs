using System;
using System.Collections.Generic;
using messaging;

namespace worker
{
    class GoodbyeMessageConsumerConfig : IMessageConsumerConfig
    {
        public Event Event => Event.HelloMessageCreated;
        public IEnumerable<string> RequestUrls => new[] {models.Urls.RequestUrls.Bye};
        public Action<IMessageBus, Message> MessageHandler => (messageBus, message) =>
                {
                    message.Data.RespondedHelloMessage = $"{message.Data.RespondedHelloMessage}, and Goodbye";
                    messageBus.Send(Event.GoodbyeMessageCreated, message);
                };
    }
}
