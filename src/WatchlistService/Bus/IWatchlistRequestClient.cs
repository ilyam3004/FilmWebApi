using TMDbLib.Objects.Movies;

namespace WatchlistService.Bus;

public interface IWatchlistRequestClient
{
    Task<string> GetUserIdFromToken(string jwt);
    Task<List<Movie>> GetMoviesData(List<int> moviesId);
}