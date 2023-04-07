using MongoDB.Driver;
using WatchlistService.Data.DbContext;
using WatchlistService.Models;

namespace WatchlistService.Data.Repositories;

public class WatchlistRepository : IWatchListRepository
{
    private readonly IWatchlistContext _context;

    public WatchlistRepository(IWatchlistContext context)
    {
        _context = context;
    }

    public async Task<Watchlist> GetWatchlistByIdAsync(string watchlistId)
    {
        return await _context.Watchlists
                .Find(w => w.Id == watchlistId)
                .FirstOrDefaultAsync();
    }

    public async Task<List<Watchlist>> GetWatchlistsAsync(string userId)
    {
         return await _context.Watchlists
                .Find(w => w.UserId == userId)
                .ToListAsync();
        
    }

    public async Task<Watchlist> CreateWatchListAsync(Watchlist watchList)
    {
        await _context.Watchlists.InsertOneAsync(watchList);
        return watchList;
    }

    public async Task<bool> UpdateWatchListAsync(Watchlist watchList)
    {
        var updateResult = await _context.Watchlists.ReplaceOneAsync(
            filter: w => w.Id == watchList.Id,
            replacement: watchList
        );

        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteWatchListAsync(string watchListId)
    {
        DeleteResult result = await _context
                .Watchlists.DeleteOneAsync(w => w.Id == watchListId);

        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public Task<bool> WatchlistExistsAsync(string watchlistName, string userId)
    {
        return _context.Watchlists
            .Find(w => w.Name == watchlistName && w.UserId == userId)
            .AnyAsync();
    }
}
