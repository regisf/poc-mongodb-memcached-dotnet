using Enyim.Caching;
using MongoDB.Bson;
using MongoDB.Driver;
using MyService.Models;

namespace MyService.Services;

public class HelloService : IHelloService
{
    private const int _pageSize = 25;
    private readonly IMongoCollection<HelloModel> _collection;
    private readonly int _expirationCache;
    private readonly IMemcachedClient _memcachedClient;

    public HelloService(
        IDatabaseSettings dbSettings,
        IMongoDatabase mongoDatabase,
        IMemcachedClient memcachedClient,
        IConfiguration configuration)
    {
        _collection = mongoDatabase.GetCollection<HelloModel>(dbSettings.HelloCollection);
        _memcachedClient = memcachedClient;
        _expirationCache = Convert.ToInt32(configuration["ExpirationTimeInMinutes"]);
    }

    public async Task<List<HelloModel>> GetAllAsync(int pageIndex)
    {
        return await _memcachedClient.GetValueOrCreateAsync($"get_all_{pageIndex}",
            _expirationCache,
            async () => await _collection
                .Find(new BsonDocument())
                .Skip(pageIndex * _pageSize)
                .Limit(_pageSize)
                .ToListAsync()
                .ConfigureAwait(false)
        );
    }

    public async Task<List<string>> GetAllGuidsAsync()
    {
        return await _memcachedClient.GetValueOrCreateAsync("get_all_guids", _expirationCache, async () =>
        {
            var cursor = await _collection.Find(_ => true).ToListAsync().ConfigureAwait(false);
            return await Task.Run(() => cursor.Select(r => r.Uid).ToList());
        });
    }

    public async Task<HelloModel> GetByUid(string uid)
    {
        return await _memcachedClient.GetValueOrCreateAsync($"get_by_uid_{uid}", _expirationCache, async () =>
            await _collection
                .Find(row => row.Uid == uid)
                .FirstAsync()
                .ConfigureAwait(false)
        );
    }
}