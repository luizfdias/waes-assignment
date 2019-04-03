using System.Collections.Generic;
using Waes.Assignment.Domain.Models;

namespace Waes.Assignment.IntegrationTests.Helpers
{
    public class CacheHelper
    {
        public static Dictionary<string, object> CreateDiff()
        {
            var diff = new EqualDiff("789456123");

            return new Dictionary<string, object>
            {
                {$"diff_{diff.CorrelationId}", diff}
            };
        }
    }
}
