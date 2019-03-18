namespace Waes.Assignment.Domain.Events
{
    public class DiffNotFoundEvent : Event
    {
        public string CorrelationId { get; }

        public DiffNotFoundEvent(string correlationId)
        {
            CorrelationId = correlationId;
        }
    }
}
