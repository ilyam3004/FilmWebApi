using Shared.Models;

namespace MovieService.Bus.Clients;

public interface IMovieRequestClient
{
    Task<List<Watchlist>> GetWatchlists(string userId);
    Task<string> GetUserIdFromToken(string jwt);
}