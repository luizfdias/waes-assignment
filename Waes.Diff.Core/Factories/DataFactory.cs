using System;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Core.Factories
{
    public static class DataFactory
    {
        public static Data Create(byte[] content, string correlationId, SideEnum side)
        {
            return new Data
            {
                Content = content,
                CorrelationId = correlationId,
                Length = content.Length,
                Side = side,
                Id = Guid.NewGuid()                
            };
        }
    }
}
