using MongoDB.Driver;
using Waes.Diff.Core.Models;

namespace Waes.Diff.Infrastructure.MongoDBStorage.Interfaces
{
    /// <summary>
    /// Contract for MongoDBContext
    /// </summary>
    public interface IMongoDBContext
    {
        /// <summary>
        /// Get the collection of Data
        /// </summary>
        IMongoCollection<Data> Data { get; }
    }
}
