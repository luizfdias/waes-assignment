using System.Threading.Tasks;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Core.Interfaces
{
    /// <summary>
    /// DiffHandler contract to orchestrate the diff analyzes
    /// </summary>
    public interface IDiffHandler
    {
        /// <summary>
        /// Starts the diff analyzes
        /// </summary>
        /// <param name="id">The identification</param>
        /// <returns>The DiffResult</returns>
        Task<DiffResult> Diff(string id);
    }
}
