using Waes.Diff.Core.Models;

namespace Waes.Diff.Core.Factories
{
    /// <summary>
    /// DifferenceFactory implementation
    /// </summary>
    public static class DifferenceFactory
    {
        /// <summary>
        /// Create a new Difference
        /// </summary>
        /// <param name="startOffset">The start index of the difference</param>
        /// <param name="length">The length of the difference</param>
        /// <returns></returns>
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
