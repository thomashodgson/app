using System;
using models;
using Serilog;

namespace messaging
{
    public class MessageObserver : IObserver<Message>
    {
        private readonly Action<Message> _onNext;
        private readonly IMessageBus _messageBus;
        private readonly ILogger _log;

        public MessageObserver(Action<Message> onNext, IMessageBus messageBus, ILogger log)
        {
            _onNext = onNext;
            _messageBus = messageBus;
            _log = log;
        }

        public void OnNext(Message message)
        {
            try
            {
                Console.WriteLine($"Starting message for: {message.RequestUrl}");
                _onNext(message);
                Console.WriteLine($"Finished message for: {message.RequestUrl}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"message for: {message.RequestUrl} caused error");
                _log.Error(e, $"{nameof(MessageObserver)}  onNext failed");

                Console.WriteLine($"error: {message.RequestUrl} logged");
                var errorMessage = BuildErrorMessage(message, e);
                Console.WriteLine($"errormessage for: {message.RequestUrl} built");

                _messageBus.Send(Event.ErrorReported, errorMessage);
                Console.WriteLine($"errormessage for: {message.RequestUrl} sent");
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
            _log.Error(error, $"{nameof(MessageObserver)}  OnError failed");
        }

        public void OnCompleted()
        {
            _log.Information($"{nameof(MessageObserver)} OnCompleted: " + _onNext);
        }
    }
}
