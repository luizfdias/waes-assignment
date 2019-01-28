using Waes.Diff.Core.Interfaces;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Core
{
    /*
        It's important to say that I assumed I could find the differences comparing de bytes of each data.
        In case any difference is found, I get the initial position and the length of that.
    */

    /// <summary>
    /// DiffChecker implementation. This class check the differences between two data
    /// </summary>
    public class BytesChecker : IDiffChecker
    {
        /// <summary>
        /// Check the differences between data
        /// </summary>
        /// <param name="leftData">The left data</param>
        /// <param name="rightData">The right data</param>
        /// <returns>Returns the DiffResult with the StartOffSet and Length of the difference</returns>
        public DiffResult Check(byte[] leftData, byte[] rightData)
        {
            var diffResult = new DiffResult();

            int diffLength = 0;
            int startOffSet = 0;

            for (int i = 0; i < leftData.Length; i++)
            {
                if (leftData[i] != rightData[i])
                {
                    if (diffLength == 0)
                        startOffSet = i;

                    diffLength += 1;

                    if (i + 1 == leftData.Length)
                        diffResult.Differences.Add(new Difference
                        {
                            Length = diffLength,
                            StartOffSet = startOffSet
                        });
                }
                else
                {
                    if (diffLength > 0)
                        diffResult.Differences.Add(new Difference
                        {
                            Length = diffLength,
                            StartOffSet = startOffSet
                        });

                    diffLength = 0;
                }
            }

            return diffResult;
        }
    }
}
