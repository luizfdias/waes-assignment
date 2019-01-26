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

        public DiffResult Check(byte[] leftBuffer, byte[] rightBuffer)
        {
            if (leftBuffer == null)
            {
                throw new ArgumentNullException(nameof(leftBuffer));
            }

            if (rightBuffer == null)
            {
                throw new ArgumentNullException(nameof(rightBuffer));
            }

            return DiffChecker.Check(leftBuffer, rightBuffer);
        }
    }
}
