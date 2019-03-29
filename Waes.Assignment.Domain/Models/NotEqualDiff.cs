using System.Collections.Generic;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Domain.Models
{
    public class NotEqualDiff : Diff
    {
        public IEnumerable<DifferenceInterval> Differences { get; }

        public NotEqualDiff(string correlationId, IEnumerable<DifferenceInterval> differences) : base(correlationId)
        {
            Differences = differences;
        }
    }
}
