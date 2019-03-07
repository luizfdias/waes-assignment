using MediatR;
using Waes.Assignment.Application.Notifications.Enums;

namespace Waes.Assignment.Application.Notifications
{
    public class WarningNotification : INotification
    {
        public string Key { get; }

        public string Value { get; }

        public NotificationType Type { get; }

        public WarningNotification(string key, string value, NotificationType type)
        {
            Key = key;
            Value = value;
            Type = type;
        }
    }
}
