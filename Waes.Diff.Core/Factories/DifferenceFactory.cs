using Waes.Diff.Core.Models;

namespace Waes.Diff.Core.Factories
{
    public static class DifferenceFactory
    {
        public static Difference Create(int startOffset, int length)
        {
            return new Difference
            {
                StartOffSet = startOffset,
                Length = length
            };
        }
    }
}
