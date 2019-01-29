using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Waes.Diff.Core.Interfaces;

namespace Waes.Diff.Infrastructure.MemoryStorage.Repositories
{
    /* 
        This is one of the alternatives to store the binary data. I chose in memory cache because would be easier
        to test the application with that. In a distributed environment, the azure blob storage or a distributed cache
        would be a better choice.
    */

    /// <summary>
    /// BinaryDataStorage implementation using in memory cache
    /// </summary>
    public class MemoryRepository : IBinaryDataStorage
    {
        public IMemoryCache MemoryCache { get; }

        public int DataExpirationInSeconds { get; }

        public MemoryRepository(IMemoryCache memoryCache, int dataExpirationInSeconds)
        {
            MemoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));

            if (dataExpirationInSeconds <= 0)
                throw new ArgumentException(nameof(dataExpirationInSeconds));

            DataExpirationInSeconds = dataExpirationInSeconds;
        }        

        public async Task<byte[]> Get(string id)
        {
            return MemoryCache.Get<byte[]>(id);
        }

        public async Task Save(string id, byte[] data)
        {            
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(DataExpirationInSeconds));

            MemoryCache.Set<byte[]>(id, data, cacheEntryOptions);
        }
    }
}
