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

    public Task AddMovieToWatchlistAsync(string watchlistId, int movieId)
    {
        return _context.Watchlists
            .UpdateOneAsync(
                w => w.Id == watchlistId,
                Builders<Watchlist>.Update.AddToSet(w => w.MoviesId, movieId)
            );
    }

    public async Task<Watchlist> CreateWatchListAsync(Watchlist watchList)
    {
        await _context.Watchlists.InsertOneAsync(watchList);
        return watchList;
    }

    public async Task<bool> DeleteWatchListAsync(string watchListId)
    {
        DeleteResult result = await _context
                .Watchlists.DeleteOneAsync(w => w.Id == watchListId);

        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public Task<bool> WatchlistExistsByIdAsync(string watchlistId)
    {
        return _context.Watchlists
            .Find(w => w.Id == watchlistId)
            .AnyAsync();
    }

    public Task<bool> WatchlistExistsByNameAsync(string userId, string watchlistName)
    {
        return _context.Watchlists
            .Find(w => w.UserId == userId && w.Name == watchlistName)
            .AnyAsync();
    }
}
