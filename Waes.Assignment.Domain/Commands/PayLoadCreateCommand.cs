using MediatR;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Domain.Commands
{
    public class PayLoadCreateCommand : IRequest<bool>
    {
        public string CorrelationId { get; set; }

        public byte[] Content { get; set; }

        public SideEnum Side { get; set; }
    }
}
