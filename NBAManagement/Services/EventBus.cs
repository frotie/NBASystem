using NBAManagement.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBAManagement.Services
{
    public class EventBus
    {
        private ConcurrentDictionary<EventSubscriber, Func<IEvent, Task>> _subscibers;
        public EventBus()
        {
            _subscibers = new ConcurrentDictionary<EventSubscriber, Func<IEvent, Task>>();
        }

        public IDisposable Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent
        {
            var task = new EventSubscriber(typeof(TEvent), e => _subscibers.TryRemove(e, out var _));

            _subscibers.TryAdd(task, (msg) => handler((TEvent)msg));

            return task;
        }

        public async Task Publish<T>(T @event) where T : IEvent
        {
            var eventType = typeof(T);

            var tasks = _subscibers
                .Where(s => s.Key.EventType == eventType)
                .Select(s => s.Value(@event));

            await Task.WhenAll(tasks);
        }
    }
}
