using HostelManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HostelManagement.Service
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IMongoClient mongoClient, IOptions<MongoDbSettings> settings)
        {
            _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName) =>
            _database.GetCollection<T>(collectionName);
    }
}
