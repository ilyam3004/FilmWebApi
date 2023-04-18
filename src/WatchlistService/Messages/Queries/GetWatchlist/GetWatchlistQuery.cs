using WatchlistService.Dtos.Responses;
using LanguageExt.Common;
using MediatR;

namespace WatchlistService.Messages.Queries.GetWatchlist;

public record GetWatchlistQuery(string WatchlistId)
    : IRequest<Result<WatchlistResponse>>;