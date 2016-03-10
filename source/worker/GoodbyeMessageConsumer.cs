using messaging;
using models.Urls;
using Serilog;

namespace worker
{
    class GoodbyeMessageConsumer : MessageConsumer
    {
        public GoodbyeMessageConsumer(IMessageBus messageBus, ILogger logger) :
            base(messageBus, logger, Event.HelloMessageCreated, RequestUrls.Bye)
        {
        }

        protected override void OnMessage(Message message)
        {
            message.Data.RespondedHelloMessage = $"{message.Data.RespondedHelloMessage}, and Goodbye";
            SendMessage(Event.GoodbyeMessageCreated, message);
        }
    }
}
