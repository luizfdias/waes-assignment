using Waes.Diff.Api.Contracts;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Api.Interfaces
{
    /// <summary>
    /// DiffResponseMapper contract used to map the DiffResult
    /// </summary>
    public interface IDiffResponseMapper
    {
        /// <summary>
        /// Map the DiffResponse from DiffResult
        /// </summary>
        /// <param name="diffResult">The DiffResult</param>
        /// <returns>The DiffResponse</returns>
        DiffResponse Map(DiffResult diffResult);
    }
}
