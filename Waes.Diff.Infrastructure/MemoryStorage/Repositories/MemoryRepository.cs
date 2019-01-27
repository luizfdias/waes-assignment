using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Waes.Diff.Core.Interfaces;

namespace Waes.Diff.Infrastructure.MemoryStorage.Repositories
{
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
