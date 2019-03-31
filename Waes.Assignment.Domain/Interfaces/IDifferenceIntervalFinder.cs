using System.Collections.Generic;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Domain.Interfaces
{
    /// <summary>
    /// Exposes the contract to find the sequences in a given int array
    /// </summary>
    public interface IDifferenceIntervalFinder
    {
        /// <summary>
        /// Finds the sequence in the given int array
        /// </summary>
        /// <param name="indexOfDifferences"></param>
        /// <returns></returns>
        IEnumerable<DifferenceInterval> Find(int[] indexOfDifferences);
    }
}
