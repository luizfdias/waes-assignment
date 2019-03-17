using MediatR;

namespace Waes.Assignment.Domain.Commands
{
    public class AnalyzeDiffCommand : Command
    {
        public string CorrelationId { get; }

        public AnalyzeDiffCommand(string correlationId)
        {
            CorrelationId = correlationId;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
