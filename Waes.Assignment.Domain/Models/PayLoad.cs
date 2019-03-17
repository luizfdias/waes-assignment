using System;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Domain.Models
{
    public class PayLoad : Entity
    {
        public string CorrelationId { get; private set; }

        public byte[] Content { get; private set; }        

        public SideEnum Side { get; private set; }

        public PayLoad(string correlationId, byte[] content, SideEnum side) : base(Guid.NewGuid())
        {
            CorrelationId = correlationId;
            Content = content;
            Side = side;
        }
    }
}
