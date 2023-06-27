using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace WatchlistService.Bus.Clients;

public interface IWatchlistRequestClient
{
    Task<string> GetUserIdFromToken(string jwt);
    Task<List<Movie>> GetMoviesData(List<int> moviesId);
    Task<List<SearchMovie>> GetWatchlistRecommendations(List<int> moviesId, int moviesCount);
}