using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Waes.Assignment.Application.Notifications.Interfaces;

namespace Waes.Assignment.Application.Notifications
{
    public class WarningNotificationHandler : INotificationHandler<WarningNotification>, IGetNotifications<WarningNotification>
    {
        private List<WarningNotification> _notifications;

        public WarningNotificationHandler()
        {
            _notifications = new List<WarningNotification>();
        }

        public IEnumerable<WarningNotification> Get()
        {
            return _notifications;
        }

        public Task Handle(WarningNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);

            return Task.CompletedTask;
        }
    }
}
