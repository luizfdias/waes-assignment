using System.Threading.Tasks;
using Waes.Assignment.Domain.Commands;
using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Application.Interfaces
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;

        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
