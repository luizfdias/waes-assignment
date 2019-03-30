using System.Threading.Tasks;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Application.Interfaces
{
    /// <summary>
    /// IMediatorHandler interface to send commands and raise events
    /// </summary>
    public interface IMediatorHandler
    {
        /// <summary>
        /// Sends a command to the bus
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        Task SendCommand<T>(T command) where T : Command;

        /// <summary>
        /// Raises an event to the bus
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        /// <returns></returns>
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
