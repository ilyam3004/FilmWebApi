using WatchlistService.Dtos.Responses;
using LanguageExt.Common;
using MediatR;

namespace WatchlistService.Messages.Queries.GetUserWatchlists;

public record GetWatchlistsQuery(string Token) 
    : IRequest<Result<List<WatchlistResponse>>>;