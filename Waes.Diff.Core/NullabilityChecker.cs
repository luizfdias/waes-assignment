using System;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Core
{
    public class NullabilityChecker : IDiffChecker
    {
        public IDiffChecker DiffChecker { get; }

        public NullabilityChecker(IDiffChecker diffChecker)
        {
            DiffChecker = diffChecker ?? throw new ArgumentNullException(nameof(diffChecker));
        }        

        public DiffResult Check(byte[] leftData, byte[] rightData)
        {
            if (leftData == null)
            {
                throw new ArgumentNullException(nameof(leftData));
            }

            if (rightData == null)
            {
                throw new ArgumentNullException(nameof(rightData));
            }

            return DiffChecker.Check(leftData, rightData);
        }
    }
}
