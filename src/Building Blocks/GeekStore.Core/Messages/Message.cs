using System;

namespace GeekStore.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; set; }
        public Message()
        {
            MessageType = GetType().Name;
        }
    }
}
