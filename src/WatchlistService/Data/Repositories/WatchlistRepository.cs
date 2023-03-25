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
    
    public async Task<Watchlist> GetWatchListsAsync(string watchlistId)
    {
        return await _context.Watchlists
            .Find(w => w.WatchlistId == watchlistId)
            .FirstOrDefaultAsync();
    }
    
    public async Task<Watchlist> CreateWatchListAsync(Watchlist watchList)
    {
        await _context.Watchlists.InsertOneAsync(watchList);
        return watchList;
    }

    public async Task<bool> UpdateWatchListAsync(Watchlist watchList)
    {
        var updateResult = await _context.Watchlists
            .ReplaceOneAsync(
                filter: w => w.WatchlistId == watchList.WatchlistId, 
                replacement: watchList);
        
        return updateResult.IsAcknowledged
               && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteWatchListAsync(string watchListId)
    {
        DeleteResult result = await _context.Watchlists
            .DeleteOneAsync(w => w.WatchlistId == watchListId);
        
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
}