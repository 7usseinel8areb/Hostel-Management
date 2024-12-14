using HostelManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace HostelManagement.Data
{
    public class ApplicationDbContext
    {
        private readonly IMongoDatabase _database;

        public ApplicationDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Room> Rooms => _database.GetCollection<Room>("Rooms");
        public IMongoCollection<Booking> Bookings => _database.GetCollection<Booking>("Bookings");
        public IMongoCollection<Feedback> Feedbacks => _database.GetCollection<Feedback>("Feedbacks");

    }
}
