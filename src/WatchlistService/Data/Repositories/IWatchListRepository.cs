using WatchlistService.Models;

namespace WatchlistService.Data.Repositories;

public interface IWatchListRepository
{
    Task<Watchlist> GetWatchlistByIdAsync(string watchlistId);
    Task<Watchlist> CreateWatchListAsync(Watchlist watchList);
    Task<bool> RemoveWatchListAsync(string watchListId);
    Task<bool> WatchlistExistsByIdAsync(string watchlistId);
    Task<bool> WatchlistExistsByNameAsync(string userId, string watchlistName);
    Task<List<Watchlist>> GetWatchlistsAsync(string userId);
    Task AddMovieToWatchlistAsync(string watchlistId, int movieId, DateTime dateTimeOfAdding);
    Task RemoveMovieFromWatchlistAsync(string watchlistId, int movieId);
    Task<bool> MovieExistsInWatchlistAsync(string watchlistId, int movieId);
}