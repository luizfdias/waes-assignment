using Waes.Diff.Core.Models;

namespace Waes.Diff.Core.Factories
{
    /// <summary>
    /// DiffResultFactory implementation
    /// </summary>
    public static class DiffResultFactory
    {
        /// <summary>
        /// Create a new DiffResult
        /// </summary>
        /// <param name="sameSize">if data are of equal size</param>
        /// <returns></returns>
        public static DiffResult Create(bool sameSize)
        {
            return new DiffResult
            {
                SameSize = sameSize
            };
        }        
    }
}
