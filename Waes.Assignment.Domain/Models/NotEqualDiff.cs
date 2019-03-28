using System.Collections.Generic;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Domain.Models
{
    public class NotEqualDiff : Diff
    {
        public IEnumerable<Differences> Differences { get; }

        public NotEqualDiff(string correlationId, IEnumerable<Differences> differences) : base(correlationId)
        {
            Differences = differences;
        }
    }
}
