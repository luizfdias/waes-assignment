using System;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Domain.Events
{
    public class PayLoadAlreadyCreatedEvent : Event
    {
        public string CorrelationId { get; }

        public SideEnum Side { get; }

        public PayLoadAlreadyCreatedEvent(Guid id, string correlationId, SideEnum sideEnum)
        {
            AggregateId = id;
            CorrelationId = correlationId;
            Side = sideEnum;
        } 
    }
}
