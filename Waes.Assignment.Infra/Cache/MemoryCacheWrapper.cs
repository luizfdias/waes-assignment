using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;

namespace Waes.Assignment.Infra.Cache
{
    /// <summary>
    /// Wrapper of <see cref="IMemoryCache"/>
    /// </summary>
    public class MemoryCacheWrapper : ICache
    {
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Initializes a new instance of <see cref="MemoryCacheWrapper"/> with <see cref="IMemoryCache"/>
        /// </summary>
        /// <param name="memoryCache"></param>
        public MemoryCacheWrapper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        /// <summary>
        /// Get <typeparamref name="T"/> in the memory cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<T> GetAsync<T>(string key)
        {
            var result = _memoryCache.TryGetValue(key, out var value) ? value : default(T);

            return Task.FromResult((T)result);
        }

        /// <summary>
        /// Set <typeparamref name="T"/> in the memory cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="cacheDurationInSeconds"></param>
        /// <returns></returns>
        public Task SetAsync<T>(string key, T value, int cacheDurationInSeconds)
        {
            _memoryCache.Set(key, value, TimeSpan.FromSeconds(cacheDurationInSeconds));

            return Task.CompletedTask;
        }
    }
}
