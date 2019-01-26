using Waes.Diff.Core.Interfaces;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Core
{
    public class BytesChecker : IDiffChecker
    {
        public DiffResult Check(byte[] leftBuffer, byte[] rightBuffer)
        {
            var diffResult = new DiffResult();

            int diffLength = 0;
            int startOffSet = 0;

            for (int i = 0; i < leftBuffer.Length; i++)
            {
                if (leftBuffer[i] != rightBuffer[i])
                {
                    if (diffLength == 0)
                        startOffSet = i;

                    diffLength += 1;

                    if (i + 1 == leftBuffer.Length)
                        diffResult.Diffs.Add(new DiffData
                        {
                            Length = diffLength,
                            StartOffSet = startOffSet
                        });
                }
                else
                {
                    if (diffLength > 0)
                        diffResult.Diffs.Add(new DiffData
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
