using System;
using System.Collections.Generic;
using System.Threading;
using messaging;

namespace worker
{
    class HelloMessageConsumerConfig : IMessageConsumerConfig
    {
        private readonly Random _random = new Random();
        public Event Event => Event.HelloRequested;
        public IEnumerable<string> RequestUrls => new[] {models.Urls.RequestUrls.Hello, models.Urls.RequestUrls.Bye};
        public Action<IMessageBus, Message> MessageHandler => (messageBus, message) =>
        {
            Thread.Sleep(1000);
            if (_random.Next(4) == 0)
            {
                throw new Exception("This is very serious");
            }

            message.Data.RespondedHelloMessage = $"Hello {message.Data.Name}";
            messageBus.Send(Event.HelloMessageCreated, message);
        };
    }
}