using WatchlistService.Dtos.Responses;
using LanguageExt.Common;
using MediatR;

namespace WatchlistService.Messages.Queries.GetUserWatchlists;

public record GetUserWatchlistsQuery(string Token) 
    : IRequest<List<WatchlistResponse>>;