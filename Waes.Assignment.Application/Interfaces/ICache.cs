using System.Threading.Tasks;

namespace Waes.Assignment.Application.Interfaces
{
    /// <summary>
    /// Abstraction that represents a cache
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Get <typeparamref name="T"/> in the cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// Set <typeparamref name="T"/> in the cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheDurationInSeconds"></param>
        /// <returns></returns>
        Task SetAsync<T>(string key, T value, int cacheDurationInSeconds);
    }
}
