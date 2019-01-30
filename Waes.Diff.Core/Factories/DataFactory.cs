using System;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Core.Factories
{
    /// <summary>
    /// DataFactory implementation
    /// </summary>
    public static class DataFactory
    {
        /// <summary>
        /// Create a new Data
        /// </summary>
        /// <param name="content">The content</param>
        /// <param name="correlationId">The correlation identification</param>
        /// <param name="side">The side (left or right)</param>
        /// <returns></returns>
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
