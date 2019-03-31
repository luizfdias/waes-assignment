using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Application.Interfaces
{
    /// <summary>
    /// INotificationEventHandler interface gets the events that had been raised by the application
    /// </summary>
    public interface INotificationHandler
    {
        /// <summary>
        /// It gets the event 
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <returns></returns>
        TEvent GetEvent<TEvent>() where TEvent : Event;
    }
}
