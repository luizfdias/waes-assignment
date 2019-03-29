using System;

namespace Waes.Assignment.Domain.Models
{
    public abstract class Diff : Entity
    {        
        public string CorrelationId { get; set; }

        public Diff(string correlationId) : base(Guid.NewGuid())
        {
            CorrelationId = correlationId;
        }
    }
}
