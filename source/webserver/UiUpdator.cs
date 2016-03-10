using System;
using messaging;
using models.Urls;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Serilog;
using webserver.Hubs;

namespace webserver
{
    class UiUpdator : IMessageConsumer
    {
        private readonly IHubContext _hubContext;
        private readonly IMessageBus _messageBus;
        private readonly ILogger _log;

        public UiUpdator(IConnectionManager connectionManager, IMessageBus messageBus, ILogger log)
        {
            _hubContext = connectionManager.GetHubContext<AppHub>();
            _messageBus = messageBus;
            _log = log;
        }

        public void Start()
        {
            SubscribeToQueue(Event.ErrorReported, "*", ReportErrorToUi);
            SubscribeToQueue(Event.HelloMessageCreated, RequestUrls.Hello, SendHelloMessageToUi);
            SubscribeToQueue(Event.GoodbyeMessageCreated, RequestUrls.Bye, SendGoodbyeMessageToUi);
        }

        private void SendGoodbyeMessageToUi(Message message)
        {
            SendMessageToUi(message);
        }


        private void SendHelloMessageToUi(Message message)
        {
           SendMessageToUi(message);
        }

        private void ReportErrorToUi(Message message)
        {
            _hubContext.ReturnUpdateToSender(message, new
            {
                Error = message.Data.Error
            });
        }

        private void SendMessageToUi(Message message)
        {
            _hubContext.ReturnUpdateToSender(message,
                new
                {
                    HelloMessageViewModel = new
                    {
                        Message = message.Data.RespondedHelloMessage,
                        IsLoading = false
                    }
                });
        }

        private void SubscribeToQueue(Event @event, string requestUrl, Action<Message> onMessageRecieved)
        {
            var uiMessagesObservable = _messageBus.GetIncomingMessageObservable(@event, requestUrl);
            uiMessagesObservable.Subscribe(new MessageObserver(onMessageRecieved, _messageBus,_log));
        }
    }
}
