using MediatR;

namespace Waes.Assignment.Domain.Commands
{
    public abstract class Command : IRequest<bool>
    {
    }
}
