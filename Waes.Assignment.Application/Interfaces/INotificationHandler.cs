using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Application.Interfaces
{
    public interface INotificationHandler
    {
        TEvent GetEvent<TEvent>() where TEvent : Event;
    }
}
