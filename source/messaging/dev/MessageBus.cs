using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace messaging.dev
{
    class MessageBus : IMessageBus
    {
        private class MessageAndTask
        {
            public Message Message { get;  }
            public Event Task { get;  }
            public MessageAndTask(Message message, Event @event)
            {
                Message = message;
                Task = @event;
            }
        }
        
        private event EventHandler<MessageAndTask> MessageEvent;
        
        public void Send(Event @event, Message message)
        {
            MessageEvent?.Invoke(this, new MessageAndTask(message, @event));
        }

        public IObservable<Message> GetIncomingMessageObservable(Event @event, string requestUrl)
        {
            return Observable.FromEventPattern<MessageAndTask>(
                h => MessageEvent += h,
                h => MessageEvent -= h
                ).Select(x => x.EventArgs).Where(x => x.Task == @event && x.Message.RequestUrl == requestUrl).Select(x => x.Message);
        }

        public IObservable<Message> GetIncomingMessageObservable(Event @event, IEnumerable<string> requestUrls)
        {
            var observables = requestUrls.Select(x => GetIncomingMessageObservable(@event, x));
            return observables.Merge();
        }
    }
}
