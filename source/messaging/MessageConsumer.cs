using System.Collections.Generic;
using Serilog;

namespace messaging
{
    public abstract class MessageConsumer : IMessageConsumer
    {
        private readonly IMessageBus _messageBus;
        private readonly ILogger _logger;
        private readonly Event _event;
        private readonly IEnumerable<string> _requestUrls;

        protected MessageConsumer(IMessageBus messageBus, ILogger logger, Event @event, string requestUrl) :
            this(messageBus, logger, @event, new []{ requestUrl }) {}

        protected MessageConsumer(IMessageBus messageBus, ILogger logger, Event @event, IEnumerable<string> requestUrls)
        {
            _messageBus = messageBus;
            _logger = logger;
            _event = @event;
            _requestUrls = requestUrls;
        }

        protected abstract void OnMessage(Message message);

        public void Start()
        {
            _messageBus.GetIncomingMessageObservable(_event, _requestUrls).Subscribe(new MessageObserver(OnMessage, _messageBus, _logger));
        }

        protected void SendMessage(Event @event, Message message)
        {
            _messageBus.Send(@event, message);
        }
    }
}