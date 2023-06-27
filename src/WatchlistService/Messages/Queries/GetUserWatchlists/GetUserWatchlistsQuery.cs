using WatchlistService.Dtos.Responses;
using MediatR;

namespace WatchlistService.Messages.Queries.GetUserWatchlists;

public record GetUserWatchlistsQuery(string Token) 
    : IRequest<List<WatchlistResponse>>;