using Waes.Diff.Core.Models;

namespace Waes.Diff.Core.Factories
{
    public static class DiffResultFactory
    {
        public static DiffResult Create(bool sameSize)
        {
            return new DiffResult
            {
                SameSize = sameSize
            };
        }        
    }
}
