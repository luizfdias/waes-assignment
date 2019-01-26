using System;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Core
{
    public class SizeChecker : IDiffChecker
    {
        public IDiffChecker DiffChecker { get; }

        public SizeChecker(IDiffChecker diffChecker)
        {
            DiffChecker = diffChecker ?? throw new ArgumentNullException(nameof(diffChecker));
        }        

        public DiffResult Check(byte[] leftBuffer, byte[] rightBuffer)
        {
            if (leftBuffer.Length != rightBuffer.Length)
            {
                return new DiffResult
                {
                    SameSize = false
                };
            }

            return DiffChecker.Check(leftBuffer, rightBuffer);
        }
    }
}
