using MongoDB.Driver;
using WatchlistService.Models;

namespace WatchlistService.Services;

public class WatchlistsService
{
    private readonly IMongoCollection<Watchlist> _watchlists;

    public WatchlistsService(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("DefaultConnection"));
        var database = client.GetDatabase("WatchlistDB");
        _watchlists = database.GetCollection<Watchlist>("Watchlists");
    }

    public async Task Create(Watchlist watchlist)
    {
        await _watchlists.InsertOneAsync(watchlist);
    }

    public async Task<List<Watchlist>> GetWatchlist(string watchlistId)
    {
        return await _watchlists
            .Find(watchlist => watchlist.WatchlistId == watchlistId)
            .ToListAsync();
    }
}