using System.Linq;
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

            // Passing through all items of the left byte array
            for (int i = 0; i < leftData.Length; i++)
            {
                // I compare the left side with the right side. If they are not equal
                if (leftData[i] != rightData[i])
                {
                    // And if it is the first occurence of a difference in a sequence
                    if (diffLength == 0)
                        // I set the startoffset
                        startOffSet = i;

                    // And then I increment the length of the it
                    diffLength += 1;

                    // In case of the bytes were all analyzed, I add the difference to be returned. It means the array was fully analyzed
                    if (i + 1 == leftData.Length)
                        diffResult.Differences.Add(new Difference
                        {
                            Length = diffLength,
                            StartOffSet = startOffSet
                        });
                }
                // If items of arrays are equal
                else
                {
                    // and the length of the difference is bigger than zero, I add a diference on the list
                    if (diffLength > 0)
                        diffResult.Differences.Add(new Difference
                        {
                            Length = diffLength,
                            StartOffSet = startOffSet
                        });

                    // And then I restart the count of the length
                    diffLength = 0;
                }
            }

            return diffResult;
        }
    }
}
