using WatchlistService.Models;
using LanguageExt.Common;
using MediatR;

namespace WatchlistService.Messages.Commands.CreateWatchlist;

public record CreateWatchlistCommand(
    string WatchlistName, 
    string Token): IRequest<Result<Watchlist>>;