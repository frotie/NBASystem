using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Services
{
    class EventSubscriber : IDisposable
    {
        // Action after disponse
        private readonly Action<EventSubscriber> _action;
        public Type EventType { get; }
        public EventSubscriber(Type eventType, Action<EventSubscriber> action)
        {
            EventType = eventType;
            _action = action;
        }
        public void Dispose()
        {
            _action?.Invoke(this);
        }
    }
    class MessageSubscriber : IDisposable
    {
        // Action after disponse
        private readonly Action<MessageSubscriber> _action;
        public Type ReceiverType { get; }
        public Type MessageType { get; }
        public MessageSubscriber(Type receiverType, Type messageType, Action<MessageSubscriber> action)
        {
            ReceiverType = receiverType;
            MessageType = messageType;
            _action = action;
        }
        public void Dispose()
        {
            _action?.Invoke(this);
        }
    }
}
