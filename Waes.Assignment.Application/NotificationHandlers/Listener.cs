using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Application.NotificationHandlers
{
    public class Listener : IListener,
        INotificationHandler<PayLoadCreatedEvent>,
        INotificationHandler<PayLoadAlreadyCreatedEvent>,
        INotificationHandler<PayLoadNotFoundEvent>
    {
        private List<Event> _events;

        public Listener()
        {
            _events = new List<Event>();
        }

        public TEvent GetEvent<TEvent>() where TEvent : Event
        {
            return _events.FirstOrDefault(x => x.GetType() == typeof(TEvent)) as TEvent;
        }

        public Task Handle(PayLoadCreatedEvent notification, CancellationToken cancellationToken)
        {
            _events.Add(notification);

            return Task.CompletedTask;
        }

        public Task Handle(PayLoadNotFoundEvent notification, CancellationToken cancellationToken)
        {
            _events.Add(notification);

            return Task.CompletedTask;
        }

        public Task Handle(PayLoadAlreadyCreatedEvent notification, CancellationToken cancellationToken)
        {
            _events.Add(notification);

            return Task.CompletedTask;
        }
    }
}
