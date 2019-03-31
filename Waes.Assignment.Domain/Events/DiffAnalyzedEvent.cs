using System;

namespace Waes.Assignment.Domain.Events
{
    /// <summary>
    /// It represents the event that is raised when a diff is analyzed
    /// </summary>
    public class DiffAnalyzedEvent : Event
    {
        /// <summary>
        /// The correlation id
        /// </summary>
        public string CorrelationId { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="DiffAnalyzedEvent"/> with id and correlationid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="correlationId"></param>
        public DiffAnalyzedEvent(Guid id, string correlationId)
        {
            EntityId = id;
            CorrelationId = correlationId;
        }
    }
}
