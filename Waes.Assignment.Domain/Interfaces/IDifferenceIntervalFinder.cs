using System.Collections.Generic;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Domain.Interfaces
{
    public interface IDifferenceIntervalFinder
    {
        IEnumerable<DifferenceInterval> Find(int[] indexOfDifferences);
    }
}
