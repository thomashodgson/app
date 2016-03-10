using System;
using System.Threading;
using messaging;
using models.Urls;
using Serilog;

namespace worker
{
    class HelloMessageConsumer : IMessageConsumer
    {
        private readonly IMessageBus _messageBus;
        private readonly ILogger _log;
        private readonly Random _random = new Random();
        public HelloMessageConsumer(IMessageBus messageBus, ILogger log)
        {
            _messageBus = messageBus;
            _log = log;
        }

        public void Start()
        {
            _messageBus.GetIncomingMessageObservable(Event.HelloRequested, new[] {RequestUrls.Hello, RequestUrls.Bye}).Subscribe(new MessageObserver(ReplyToHello, _messageBus, _log));
            _messageBus.GetIncomingMessageObservable(Event.HelloMessageCreated, RequestUrls.Bye).Subscribe(new MessageObserver(AddGoodBye, _messageBus, _log));
        }

        private void AddGoodBye(Message message)
        {
           message.Data.RespondedHelloMessage = $"{message.Data.RespondedHelloMessage}, and Goodbye";
           _messageBus.Send(Event.GoodbyeMessageCreated, message);
        }

        private void ReplyToHello(Message message)
        {
//            Thread.Sleep(5000);
//            if (_random.Next(4) == 0)
//            {
//                throw new Exception("This is very serious");
//            }
//            
            message.Data.RespondedHelloMessage = $"Hello {message.Data.Name}"; 
            _messageBus.Send(Event.HelloMessageCreated, message);
        }
    }
}
