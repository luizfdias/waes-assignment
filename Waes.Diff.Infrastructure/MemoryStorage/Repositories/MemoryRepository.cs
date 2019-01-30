using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Infrastructure.MemoryStorage.Repositories
{
    /* 
        This is one of the alternatives to store the data. I chose in memory cache because would be easier
        to test the application with that. In a distributed environment, the azure blob storage or a distributed cache
        would be a better choice.
    */

    /// <summary>
    /// DataStorage implementation using in memory cache
    /// </summary>
    public class MemoryRepository : IDataStorage
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

        public async Task Save(Data data)
        {            
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(DataExpirationInSeconds));

            MemoryCache.Set(data.CorrelationId + data.Side.ToString(), data, cacheEntryOptions);
        }

        public async Task<IEnumerable<Data>> GetByCorrelationId(string correlationId)
        {
            var data = new List<Data>();

            var leftData = MemoryCache.Get<Data>(correlationId + SideEnum.Left);
            var rightData = MemoryCache.Get<Data>(correlationId + SideEnum.Right);

            if (leftData != null)
                data.Add(leftData);

            if (rightData != null)
                data.Add(rightData);

            return data;
        }
    }
}
