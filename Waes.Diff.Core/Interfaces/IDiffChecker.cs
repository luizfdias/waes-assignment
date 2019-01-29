﻿using Waes.Diff.Core.Models;

namespace Waes.Diff.Core.Interfaces
{
    /// <summary>
    /// DiffChecker contract to analyse the differences between data.
    /// </summary>
    /// <remarks>
    /// This contract has three implementations:
    /// 1 - NullabilityChecker - Used to validate if any of the data are null
    /// 2 - SizeChecker - Used to check if the data have the same size
    /// 3 - BytesChecker - Used to find where the differences between data are
    /// They are composed in the following order: NullabilityChecker -> SizeChecker -> BytesChecker
    /// </remarks>
    public interface IDiffChecker
    {
        /// <summary>
        /// Check the Diff
        /// </summary>
        /// <param name="leftData">The left data</param>
        /// <param name="rightData">The right data</param>
        /// <returns>The result of the diff</returns>
        DiffResult Check(byte[] leftData, byte[] rightData);
    }
}
