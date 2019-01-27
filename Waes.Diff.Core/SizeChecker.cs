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

        /// <summary>
        /// This method check the sizes of the datas.
        /// </summary>
        /// <param name="leftData"></param>
        /// <param name="rightData"></param>
        /// <returns></returns>
        public DiffResult Check(byte[] leftData, byte[] rightData)
        {
            //// I assumed that if the size is different, it is not necessary to compare the data itself.
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
