using System;
using System.Collections.Generic;
using System.Linq;
using Waes.Assignment.Domain.Interfaces;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Domain.Models
{
    public class DiffEngine : IDiffEngine
    {        
        public Diff ProcessDiff<TEquatable>(string correlationId, TEquatable[] left, TEquatable[] right) where TEquatable : IEquatable<TEquatable>
        {
            if (left.Count() != right.Count())
                return new NotOfEqualSizeDiff(correlationId);

            var diffPositions = new List<int>();

            for (int i = 0; i < left.Length; i++)
            {
                if (!left[i].Equals(right[i]))
                {
                    diffPositions.Add(i);
                }
            }

            if (diffPositions.Any())
            {
                return new NotEqualDiff(correlationId, GetDifferenceSequences(diffPositions.ToArray()));
            }

            return new EqualDiff(correlationId);
        }

        private IEnumerable<Differences> GetDifferenceSequences(int[] positions)
        {            
            var sequences = new List<Differences>();

            int currentLen = 1, currentIdx = 0;

            for (int i = 0; i < positions.Length; i++)
            {
                int nextIndex = i + 1;

                if (nextIndex < positions.Length && positions[i] + 1 == positions[nextIndex])
                {
                    currentLen++;
                }
                else
                {
                    sequences.Add(new Differences(currentIdx, currentLen));
                    currentIdx = nextIndex;

                    currentLen = 1;
                }
            }

            return sequences;
        }
    }
}
