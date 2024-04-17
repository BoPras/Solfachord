using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApplicationMain.Models;

namespace WebApplicationMain.Services
{
    public class LogServices : ILogServices
    {
        private readonly IMongoCollection<Log> _log;
        private readonly IOptions<MongoSettings> _mongoSettings;

        public LogServices(IOptions<MongoSettings> options)
        {
            _mongoSettings = options;
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            _log = mongoDatabase.GetCollection<Log>("history");
        }

        public async Task CreateAsync(Log log) =>
             await _log.InsertOneAsync(log);
    }
}
