using WatchlistService.Dtos.Requests;
using WatchlistService.Dtos.Responses;
using WatchlistService.Models;
using LanguageExt.Common;

namespace WatchlistService.Common.Services;

public interface IWatchlistService
{
    Task<Result<CreateWatchlistResponse>> CreateWatchlist(CreateWatchlistRequest request, string token);
    Task<Result<List<Watchlist>>> GetWatchlists(string token);
    Task<Result<Watchlist>> GetWatchlistByIdAsync(string watchlistId);
}