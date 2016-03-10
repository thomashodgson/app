using System;
using System.Collections.Generic;

namespace messaging
{
    public interface IMessageBus
    {
        void Send(Event @event, Message message);
        IObservable<Message> GetIncomingMessageObservable(Event @event, string requestUrl);
        IObservable<Message> GetIncomingMessageObservable(Event @event, IEnumerable<string> requestUrls);
    }
}
