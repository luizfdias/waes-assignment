using System;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Domain.Events
{
    /// <summary>
    /// It represents the event that is raised when a payload is created
    /// </summary>
    public class PayLoadCreatedEvent : Event 
    {
        /// <summary>
        /// The correlation id
        /// </summary>
        public string CorrelationId { get; }

        /// <summary>
        /// The content
        /// </summary>
        public byte[] Content { get; }

        /// <summary>
        /// The side
        /// </summary>
        public SideEnum Side { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="PayLoadCreatedEvent"/> with id, correlationid, content and side
        /// </summary>
        /// <param name="id"></param>
        /// <param name="correlationId"></param>
        /// <param name="content"></param>
        /// <param name="side"></param>
        public PayLoadCreatedEvent(Guid id, string correlationId, byte[] content, SideEnum side)
        {
            EntityId = id;
            CorrelationId = correlationId;
            Content = content;
            Side = side;
        }        
    }
}
