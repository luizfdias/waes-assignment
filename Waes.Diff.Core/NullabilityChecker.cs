using System;
using Waes.Diff.Core.Exceptions;
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
        /// Check if data are null. If they are, throw an DataNotFoundException
        /// </summary>
        /// <param name="leftData">The left data</param>
        /// <param name="rightData">The right data</param>
        /// <returns>The result of the diff</returns>
        public DiffResult Check(Data leftData, Data rightData)
        {
            if (leftData == null || rightData == null)
            {
                throw new DataNotFoundException();
            }

            return DiffChecker.Check(leftData, rightData);
        }
    }
}
