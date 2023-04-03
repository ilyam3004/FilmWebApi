using WatchlistService.Dtos.Requests;
using WatchlistService.Models;
using LanguageExt.Common;

namespace WatchlistService.Common.Services;

public interface IWatchlistService
{
    Task<Result<Watchlist>> CreateWatchlist(CreateWatchlistRequest request, string token);
}