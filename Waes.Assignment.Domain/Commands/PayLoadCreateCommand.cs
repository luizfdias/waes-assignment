using MediatR;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Domain.Commands
{
    public class PayLoadCreateCommand : Command
    {
        public string CorrelationId { get; }

        public byte[] Content { get; }

        public SideEnum Side { get; }

        public PayLoadCreateCommand(string correlationId, byte[] content, SideEnum side)
        {
            CorrelationId = correlationId;
            Content = content;
            Side = side;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
