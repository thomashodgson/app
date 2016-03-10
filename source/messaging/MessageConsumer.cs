﻿using System;
using System.Collections.Generic;
using models;
using Serilog;

namespace messaging
{
    public abstract class MessageConsumer : IMessageConsumer, IObserver<Message>
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
            _messageBus.GetIncomingMessageObservable(_event, _requestUrls).Subscribe(this);
        }

        protected void SendMessage(Event @event, Message message)
        {
            _messageBus.Send(@event, message);
        }

        private string GetMessageConsumerName()
        {
            return GetType().Name;
        }

        private void LogInfo(Message message, string logMessage)
        {
            _logger.Information($"MessageId: {message.Id} Message Consumer: {GetMessageConsumerName()}, request url: {message.RequestUrl}: {logMessage}");
        }

        public void OnNext(Message message)
        {
            try
            {
                LogInfo(message, "Message received");
                OnMessage(message);
                LogInfo(message, "Message processed");
            }
            catch (Exception e)
            {
                _logger.Error(e, "Exception during message processing");

                var errorMessage = BuildErrorMessage(message, e);

                _messageBus.Send(Event.ErrorReported, errorMessage);
            }
        }

        private static Message BuildErrorMessage(Message message, Exception e)
        {
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var secondsSinceEpoch = (long)t.TotalMilliseconds;

            message.Data.Error = new ErrorReport(e.Message, secondsSinceEpoch);

            return message;
        }


        public void OnError(Exception error)
        {
            _logger.Error(error, $"{GetMessageConsumerName()} subscription OnError");
        }

        public void OnCompleted()
        {
            _logger.Information($"{GetMessageConsumerName()} subscription OnCompleted");
        }
    }
}