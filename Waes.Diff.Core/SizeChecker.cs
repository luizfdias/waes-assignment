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
        /// <param name="leftBuffer"></param>
        /// <param name="rightBuffer"></param>
        /// <returns></returns>
        public DiffResult Check(byte[] leftBuffer, byte[] rightBuffer)
        {
            //// I assumed that if the size is different, it is not necessary to compare the data itself.
            if (leftBuffer.Length != rightBuffer.Length)
            {
                return new DiffResult
                {
                    SameSize = false
                };
            }

            var result = DiffChecker.Check(leftBuffer, rightBuffer);

            result.SameSize = true;

            return result;
        }
    }
}
