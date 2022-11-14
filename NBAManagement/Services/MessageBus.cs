using NBAManagement.Messages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Services
{
    public class MessageBus
    {
        // MessageSubscriber - информация об ожидании сообщениий (кем, какого типа)
        // Func - делегат действия при его получении (Task с аргументом IMessage)
        private ConcurrentDictionary<MessageSubscriber, Func<IMessage, Task>> _consumers;
        public MessageBus()
        {
            _consumers = new ConcurrentDictionary<MessageSubscriber, Func<IMessage, Task>>();
        }

        public async Task SendTo<TReceiver>(IMessage message)
        {
            var msgType = message.GetType();
            var rcvType = typeof(TReceiver);

            var tasks = _consumers
                .Where(s => s.Key.MessageType == msgType && s.Key.ReceiverType == rcvType)
                .Select(s => s.Value(message));

            await Task.WhenAll(tasks);
        }

        // IDisposable для оповещения об удалении экземпляра MessageSubscriber
        public IDisposable Receive<TMessage>(object receiver, Func<TMessage, Task> handler) where TMessage : IMessage
        {
            var task = new MessageSubscriber(receiver.GetType(), typeof(TMessage), s => _consumers.TryRemove(s, out var _));
            // При удалении TryRemove возвращает значение по ключу в никуда (out var _)

            _consumers.TryAdd(task, (@message) => handler((TMessage)@message));

            return task;
        }
    }
}
