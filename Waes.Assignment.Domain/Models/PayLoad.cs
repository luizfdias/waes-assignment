using System;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Domain.Models
{
    public class PayLoad : IEntity
    {
        public Guid Id { get; }

        public string CorrelationId { get; }

        public byte[] Content { get; }        

        public SideEnum Side { get; }

        public PayLoad(Guid id, string correlationId, byte[] content, SideEnum side)
        {            
            Id = id;
            CorrelationId = correlationId;
            Content = content;
            Side = side;
        }

        public int GetSize()
        {
            return Content.Length;
        }
    }
}
