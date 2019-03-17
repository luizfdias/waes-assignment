using System;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Domain.Events
{
    public class PayLoadCreatedEvent : Event 
    {
        public string CorrelationId { get; }
        public byte[] Content { get; }
        public SideEnum Side { get; }

        public PayLoadCreatedEvent(Guid id, string correlationId, byte[] content, SideEnum side)
        {
            AggregateId = id;
            CorrelationId = correlationId;
            Content = content;
            Side = side;
        }        
    }
}
