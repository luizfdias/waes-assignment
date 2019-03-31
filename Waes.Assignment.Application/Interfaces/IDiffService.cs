using System.Threading.Tasks;
using Waes.Assignment.Application.ApiModels;

namespace Waes.Assignment.Application.Interfaces
{
    /// <summary>
    /// IDiffService interface gets the difference already analyzed between payloads
    /// </summary>
    public interface IDiffService
    {
        /// <summary>
        /// Gets the diff as a <see cref="DiffResponse"/>
        /// </summary>
        /// <param name="correlationId"></param>
        /// <returns></returns>
        Task<DiffResponse> Get(string correlationId);
    }
}
