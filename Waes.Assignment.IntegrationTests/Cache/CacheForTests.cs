using System.Collections.Generic;
using System.Threading.Tasks;
using Waes.Assignment.Application.Interfaces;

namespace Waes.Assignment.IntegrationTests.Cache
{
    public class CacheForTests : ICache
    {
        private readonly Dictionary<string, object> _objectsCached;

        public CacheForTests(Dictionary<string, object> objectsCached)
        {
            _objectsCached = objectsCached;
        }

        public Task<T> GetAsync<T>(string key)
        {
            var result = _objectsCached.ContainsKey(key) ? (T)_objectsCached[key] : default(T);

            return Task.FromResult(result);
        }

        public Task SetAsync<T>(string key, T value, int cacheDurationInSeconds)
        {
            if (!_objectsCached.ContainsKey(key))
            {
                _objectsCached.Add(key, value);
            }

            return Task.CompletedTask;
        }
    }
}
