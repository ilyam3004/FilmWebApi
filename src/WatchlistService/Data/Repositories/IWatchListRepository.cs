using WatchlistService.Models;

namespace WatchlistService.Data.Repositories;

public interface IWatchListRepository
{
    Task<Watchlist> GetWatchListsAsync(string watchlistId);
    Task<Watchlist> CreateWatchListAsync(Watchlist watchList);
    Task<bool> UpdateWatchListAsync(Watchlist watchList);
    Task<bool> DeleteWatchListAsync(string watchListId);
}