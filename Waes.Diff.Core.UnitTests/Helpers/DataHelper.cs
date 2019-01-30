using System;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Core.UnitTests.Helpers
{
    public static class DataHelper
    {
        public static Data CreateData(byte[] content, int length, Core.Models.SideEnum side)
        {
            return new Data
            {
                Content = content,
                Id = Guid.NewGuid(),
                CorrelationId = "123456789",
                Length = length,
                Side = side
            };
        }
    }
}
