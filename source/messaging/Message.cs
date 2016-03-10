
using System;
using models;

namespace messaging
{
    public class Message
    {
        public string RequestUrl { get; set; }
        public MessageData Data { get; }
        public User User { get; }
        public Guid Id { get; }

        public Message(Guid? id, string requestUrl, User user, MessageData data)
        {
            RequestUrl = requestUrl;
            Data = data ?? new MessageData();
            User = user;
            Id = id ?? Guid.NewGuid();
        }
    }
}