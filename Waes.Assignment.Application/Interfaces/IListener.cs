using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Application.Interfaces
{
    public interface IListener
    {
        TEvent GetEvent<TEvent>() where TEvent : Event;
    }
}
