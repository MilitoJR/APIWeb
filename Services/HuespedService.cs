using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MiApiWeb.Models;

namespace MiApiWeb.Services
{
    public class HuespedService
    {
        private readonly IMongoCollection<huesped> _huespedCollection;
        public HuespedService(IOptions<HuespedDBSettings> huespedDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                huespedDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                huespedDatabaseSettings.Value.DatabaseName);
            _huespedCollection = mongoDatabase.GetCollection<huesped>(
                huespedDatabaseSettings.Value.CollectionName);
        }
        public async Task<List<huesped>> GetAsync() =>
            await _huespedCollection.Find(_ => true).ToListAsync();
        public async Task<huesped?> GetAsync(string id) =>
            await _huespedCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(huesped newhuesped) =>
            await _huespedCollection.InsertOneAsync(newhuesped);
        public async Task UpdateAsync(string id, huesped updatedHuesped) =>
            await _huespedCollection.ReplaceOneAsync(x => x.Id == id, updatedHuesped);
        public async Task RemoveAsync(string id) =>
            await _huespedCollection.DeleteOneAsync(x => x.Id == id);
    }
   
    
}

