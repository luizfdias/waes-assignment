using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Waes.Diff.Core.Models;
using Waes.Diff.Infrastructure.MongoDBStorage.Factories;
using Waes.Diff.Infrastructure.MongoDBStorage.Interfaces;

namespace Waes.Diff.Infrastructure.MongoDBStorage
{
    /// <summary>
    /// MongoDBContext Implementation
    /// </summary>
    public class MongoDBContext : IMongoDBContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public StorageSettings StorageSettings { get; }

        public IMongoCollection<Data> Data => _mongoDatabase.GetCollection<Data>("DataInfo");

        public MongoDBContext(IOptions<StorageSettings> storageSettings)
        {
            StorageSettings = storageSettings?.Value ?? throw new ArgumentNullException(nameof(storageSettings));

            _mongoDatabase = MongoDBFactory.Create(StorageSettings.ConnectionString, StorageSettings.Database);
        }
    }
}
