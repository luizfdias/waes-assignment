using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Application.NotificationHandlers
{
    public class NotificationHandler : INotificationHandler,
        INotificationHandler<PayLoadCreatedEvent>,
        INotificationHandler<PayLoadAlreadyCreatedEvent>,
        INotificationHandler<DiffNotFoundEvent>,
        INotificationHandler<DiffAnalyzedEvent>
    {
        private List<Event> _events;

        public NotificationHandler()
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

        public Task Handle(PayLoadAlreadyCreatedEvent notification, CancellationToken cancellationToken)
        {
            _events.Add(notification);

            return Task.CompletedTask;
        }

        public Task Handle(DiffNotFoundEvent notification, CancellationToken cancellationToken)
        {
            _events.Add(notification);

            return Task.CompletedTask;
        }

        public Task Handle(DiffAnalyzedEvent notification, CancellationToken cancellationToken)
        {
            _events.Add(notification);

            return Task.CompletedTask;
        }
    }
}
