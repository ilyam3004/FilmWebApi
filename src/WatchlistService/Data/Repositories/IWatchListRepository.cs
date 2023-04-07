using WatchlistService.Models;

namespace WatchlistService.Data.Repositories;

public interface IWatchListRepository
{
    Task<Watchlist> GetWatchlistByIdAsync(string watchlistId);
    Task<Watchlist> CreateWatchListAsync(Watchlist watchList);
    Task<bool> UpdateWatchListAsync(Watchlist watchList);
    Task<bool> DeleteWatchListAsync(string watchListId);
    Task<bool> WatchlistExistsAsync(string watchlistName, string userId);
    Task<List<Watchlist>> GetWatchlistsAsync(string userId);
}
