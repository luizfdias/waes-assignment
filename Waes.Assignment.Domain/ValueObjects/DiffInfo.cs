using System.Collections.Generic;
using System.Linq;
using Waes.Assignment.Domain.Models.Enums;
using Waes.Assignment.Domain.ValueObjects;

namespace Waes.Assignment.Domain.ValueObjects
{
    public class DiffInfo 
    {
        public IEnumerable<DiffPosition> DiffPositions { get; }

        public DiffStatus Status { get; }

        public DiffInfo(DiffStatus status, IEnumerable<DiffPosition> diffPositions)
        {
            Status = status;
            DiffPositions = diffPositions;
        }

        public IEnumerable<DiffSequence> GetSequenceOfDifferences()
        {
            var sequences = new List<DiffSequence>();

            var array = DiffPositions.ToArray();

            int currentLen = 1, currentIdx = 0;

            for (int i = 0; i < array.Length; i++)
            {
                int nextIndex = i + 1;

                if (nextIndex < array.Length && array[i].Position + 1 == array[nextIndex].Position)
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

        #region Factory methods
        public static DiffInfo CreateEqual()
        {
            return new DiffInfo(DiffStatus.Equal, null);
        }

        public static DiffInfo CreateNotOfEqualSize()
        {
            return new DiffInfo(DiffStatus.NotOfEqualSize, null);
        }

        public static DiffInfo CreateNotEqual(IEnumerable<DiffPosition> diffPositions)
        {
            return new DiffInfo(DiffStatus.NotEqual, diffPositions);
        }
        #endregion
    }
}
