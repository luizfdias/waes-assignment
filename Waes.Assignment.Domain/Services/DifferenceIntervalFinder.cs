using System.Collections.Generic;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Domain.Services
{
    /// <summary>
    /// It is responsible for find the sequence in a given int array
    /// </summary>
    public class DifferenceIntervalFinder : IDifferenceIntervalFinder
    {
        /// <summary>
        /// Finds the sequence in the given int array. If there is no sequence, it returns an empty list
        /// </summary>
        /// <param name="indexOfDifferences"></param>
        /// <returns></returns>
        public IEnumerable<DifferenceInterval> Find(int[] indexOfDifferences)
        {
            var sequences = new List<DifferenceInterval>();

            int currentLen = 1, currentIdx = 0;

            for (int i = 0; i < indexOfDifferences.Length; i++)
            {
                int nextIndex = i + 1;

                if (nextIndex < indexOfDifferences.Length && indexOfDifferences[i] + 1 == indexOfDifferences[nextIndex])
                {
                    currentLen++;
                }
                else
                {
                    sequences.Add(new DifferenceInterval(currentIdx, currentLen));
                    currentIdx = nextIndex;

                    currentLen = 1;
                }
            }

            return sequences;
        }
    }
}
