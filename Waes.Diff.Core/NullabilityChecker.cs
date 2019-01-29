using System;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Core
{
    /// <summary>
    /// DiffChecker implementation. This class check if the params are null
    /// </summary>
    public class NullabilityChecker : IDiffChecker
    {
        public IDiffChecker DiffChecker { get; }

        public NullabilityChecker(IDiffChecker diffChecker)
        {
            DiffChecker = diffChecker ?? throw new ArgumentNullException(nameof(diffChecker));
        }

        /// <summary>
        /// Check if data are null. If they are, throw an ArgumentNullException
        /// </summary>
        /// <param name="leftData">The left data</param>
        /// <param name="rightData">The right data</param>
        /// <returns>The result of the diff</returns>
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
