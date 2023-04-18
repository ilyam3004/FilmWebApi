using LanguageExt.Common;
using MediatR;
using WatchlistService.Dtos.Responses;

namespace WatchlistService.Messages.Commands.RemoveWatchlist;

public record RemoveWatchlistCommand(
    string WatchlistId,
    string Token) : IRequest<Result<Deleted>>;