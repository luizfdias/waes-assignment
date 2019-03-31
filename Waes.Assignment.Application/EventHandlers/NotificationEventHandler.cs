using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Waes.Assignment.Application.Interfaces;
using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Application.EventHandlers
{
    /// <summary>
    /// NotificationEventHandler acts as an in memory events storage. It keeps all events raised during the request time of some operation.    
    /// </summary>
    public class NotificationEventHandler : INotificationHandler,
        INotificationHandler<PayLoadCreatedEvent>,
        INotificationHandler<DiffAnalyzedEvent>
    {
        private readonly List<Event> _events;

        /// <summary>
        /// Initializes a new instance of <see cref="NotificationEventHandler"/>
        /// </summary>
        public NotificationEventHandler()
        {
            _events = new List<Event>();
        }

        /// <summary>
        /// It gets the event 
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <returns>The event or null if it not found</returns>
        public TEvent GetEvent<TEvent>() where TEvent : Event
        {
            return _events.FirstOrDefault(x => x.GetType() == typeof(TEvent)) as TEvent;
        }

        /// <summary>
        /// It handles a <see cref="PayLoadCreatedEvent"/> adding it to the list of events
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(PayLoadCreatedEvent notification, CancellationToken cancellationToken)
        {
            _events.Add(notification);

            return Task.CompletedTask;
        }

        /// <summary>
        /// It handles a <see cref="DiffAnalyzedEvent"/> adding it to the list of events
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Handle(DiffAnalyzedEvent notification, CancellationToken cancellationToken)
        {
            _events.Add(notification);

            return Task.CompletedTask;
        }
    }
}
