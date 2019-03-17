using System;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Domain.Models
{
    public class Diff : Entity
    {
        public string CorrelationId { get; private set; }

        public DiffInfo Info { get; private set; }

        public Diff(string correlationId, DiffInfo info) : base(Guid.NewGuid())
        {
            CorrelationId = correlationId;
            Info = info;
        }
    }
}
