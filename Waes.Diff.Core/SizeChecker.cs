using System;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Core
{
    /// <summary>
    /// DiffChecker implementation. This class check the size of data
    /// </summary>
    public class SizeChecker : IDiffChecker
    {
        public IDiffChecker DiffChecker { get; }

        public SizeChecker(IDiffChecker diffChecker)
        {
            DiffChecker = diffChecker ?? throw new ArgumentNullException(nameof(diffChecker));
        }

        /// <summary>
        /// Check the size of the data
        /// </summary>
        /// <param name="leftData">The left data</param>
        /// <param name="rightData">The right data</param>
        /// <returns>The result of the diff</returns>
        public DiffResult Check(Data leftData, Data rightData)
        {            
            //// I assumed for this assignment, if the data size is different, it is not necessary to compare the data itself.
            if (leftData.Length != rightData.Length)
            {
                return new DiffResult
                {
                    SameSize = false                    
                };
            }

            var result = DiffChecker.Check(leftData, rightData);

            result.SameSize = true;

            return result;
        }
    }
}
