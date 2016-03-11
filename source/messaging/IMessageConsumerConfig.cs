using System;
using System.Collections.Generic;

namespace messaging
{
    public interface IMessageConsumerConfig
    {
        Event Event { get; }
        IEnumerable<string> RequestUrls { get; } 
        Action<IMessageBus, Message> MessageHandler { get; }
    }
}