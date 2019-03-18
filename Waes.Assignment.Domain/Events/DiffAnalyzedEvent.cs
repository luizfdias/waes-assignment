using Waes.Assignment.Domain.Events;

namespace Waes.Assignment.Domain.Events
{
    public class DiffAnalyzedEvent : Event
    {
        public string CorrelationId { get; }

        public DiffAnalyzedEvent(string correlationId)
        {
            CorrelationId = correlationId;
        }
    }
}
