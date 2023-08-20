using MongoDB.Driver;

namespace Stock.API.Services
{
    public class MongoDBService
    {
        readonly IMongoDatabase _database;
        public MongoDBService(IConfiguration configuration)
        {
            MongoClient client = new(configuration.GetConnectionString("MongoDB"));
            _database = client.GetDatabase("StockAPIDB");
        }

        public IMongoCollection<T> GetCollection<T>() => _database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }
}
