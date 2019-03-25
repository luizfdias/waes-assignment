using System.Collections.Generic;
using System.Linq;
using Waes.Assignment.Domain.Models.Enums;

namespace Waes.Assignment.Domain.ValueObjects
{
    public class DiffInfo 
    {
        public DiffPosition[] DiffPositions { get; }

        public DiffStatus Status { get; }

        public DiffInfo(DiffStatus status)
        {
            Status = status;
        }

        public DiffInfo(DiffStatus status, DiffPosition[] diffPositions)
        {
            Status = status;
            DiffPositions = diffPositions;
        }

        public IEnumerable<DiffSequence> GetSequenceOfDifferences()
        {
            if (DiffPositions == null || !DiffPositions.Any())
                return new List<DiffSequence>();

            var sequences = new List<DiffSequence>();

            int currentLen = 1, currentIdx = 0;

            for (int i = 0; i < DiffPositions.Length; i++)
            {
                int nextIndex = i + 1;

                if (nextIndex < DiffPositions.Length && DiffPositions[i].Position + 1 == DiffPositions[nextIndex].Position)
                {
                    currentLen++;                                       
                }
                else
                {
                    sequences.Add(new DiffSequence(currentIdx, currentLen));
                    currentIdx = nextIndex;

                    currentLen = 1;
                }
            }

            return sequences;
        }
    }
}
