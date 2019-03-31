using MediatR;

namespace Waes.Assignment.Domain.Commands
{
    /// <summary>
    /// Command abstraction
    /// </summary>
    public abstract class Command : IRequest<bool>
    {
    }
}
