using System.Collections.Generic;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Domain.Services
{
    public class DifferenceIntervalFinder : IDifferenceIntervalFinder
    {
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
