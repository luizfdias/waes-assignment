using System.Collections.Generic;

namespace Waes.Assignment.Application.Notifications.Interfaces
{
    public interface IGetNotifications<TNotification>
    {
        IEnumerable<TNotification> Get();
    }
}
