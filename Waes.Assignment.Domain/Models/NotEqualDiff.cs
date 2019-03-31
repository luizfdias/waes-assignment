using System.Collections.Generic;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Domain.Models
{
    /// <summary>
    /// It represents the result of the diff when comparsion between objects are not equal
    /// </summary>
    public class NotEqualDiff : Diff
    {
        /// <summary>
        /// Differences interval
        /// </summary>
        public IEnumerable<DifferenceInterval> Differences { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="NotEqualDiff"/> with correlationid and differences interval
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="differences"></param>
        public NotEqualDiff(string correlationId, IEnumerable<DifferenceInterval> differences) : base(correlationId)
        {
            Differences = differences;
        }
    }
}
