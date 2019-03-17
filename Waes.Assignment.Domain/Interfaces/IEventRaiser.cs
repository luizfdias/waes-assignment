using System.Threading.Tasks;
using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Domain.Interfaces
{
    public interface IEventRaiser
    {
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
