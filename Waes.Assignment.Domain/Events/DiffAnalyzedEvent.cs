using System;

namespace Waes.Assignment.Domain.Events
{
    public class DiffAnalyzedEvent : Event
    {
        public string CorrelationId { get; }

        public DiffAnalyzedEvent(Guid id, string correlationId)
        {
            AggregateId = id;
            CorrelationId = correlationId;
        }
    }
}
