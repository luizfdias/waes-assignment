using System.Threading.Tasks;
using Waes.Assignment.Application.ApiModels;

namespace Waes.Assignment.Application.Interfaces
{
    /// <summary>
    /// IPayLoadService interface services the creation of new payloads
    /// </summary>
    public interface IPayLoadService
    {
        /// <summary>
        /// Creates a new payload
        /// </summary>
        /// <param name="correlationId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CreatePayLoadResponse> Create(string correlationId, CreatePayLoadRequest request);
    }
}
