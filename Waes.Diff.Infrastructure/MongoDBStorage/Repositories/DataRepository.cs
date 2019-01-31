using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Waes.Diff.Core.Interfaces;
using Waes.Diff.Core.Models;
using Waes.Diff.Infrastructure.MongoDBStorage.Exceptions;
using Waes.Diff.Infrastructure.MongoDBStorage.Interfaces;

namespace Waes.Diff.Infrastructure.MongoDBStorage.Repositories
{
    /// <summary>
    /// Implementation of IDataStorage
    /// </summary>
    public class DataRepository : IDataStorage
    {
        private readonly IMongoDBContext _mongoDBContext;

        public DataRepository(IMongoDBContext mongoDBContext)
        {
            _mongoDBContext = mongoDBContext ?? throw new System.ArgumentNullException(nameof(mongoDBContext));
        }        

        public async Task<IEnumerable<Data>> GetByCorrelationId(string correlationId)
        {            
            return await _mongoDBContext.Data.Find(x => x.CorrelationId == correlationId).ToListAsync();
        }

        /// <summary>
        /// Save data in Storage. Case the data already exists, throw an CorrelationIdAlreadyUsedForDataException.
        /// </summary>
        /// <param name="data">The data</param>
        public async Task Save(Data data)
        {
            var dataFromDatabase = await GetByCorrelationId(data.CorrelationId);

            if (dataFromDatabase.Any(x => x.Side == data.Side))
                throw new CorrelationIdAlreadyUsedForDataException(data.CorrelationId, data.Side);

            await _mongoDBContext.Data.InsertOneAsync(data);
        }
    }
}
