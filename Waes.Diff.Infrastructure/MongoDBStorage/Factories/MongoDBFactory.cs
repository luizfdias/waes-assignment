using MongoDB.Driver;

namespace Waes.Diff.Infrastructure.MongoDBStorage.Factories
{
    public static class MongoDBFactory
    {
        public static IMongoDatabase Create(string connectionString, string dataBase)
        {
            var client = new MongoClient(connectionString);

            return client.GetDatabase(dataBase);
        }
    }
}
