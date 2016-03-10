using messaging;
using models.Urls;
using Serilog;

namespace worker
{
    class HelloMessageConsumer : MessageConsumer
    {
        public HelloMessageConsumer(IMessageBus messageBus, ILogger logger) :
            base(messageBus, logger, Event.HelloRequested, new[] { RequestUrls.Hello, RequestUrls.Bye })
        {
        }

        protected override void OnMessage(Message message)
        {

            //            Thread.Sleep(5000);
            //            if (_random.Next(4) == 0)
            //            {
            //                throw new Exception("This is very serious");
            //            }

            message.Data.RespondedHelloMessage = $"Hello {message.Data.Name}";
            SendMessage(Event.HelloMessageCreated, message);
        }
    }
}