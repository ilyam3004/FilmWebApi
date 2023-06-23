using WatchlistService.Dtos.Responses;
using LanguageExt.Common;
using MediatR;
using WatchlistService.Bus;
using WatchlistService.Bus.Clients;
using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;

namespace WatchlistService.Messages.Commands.RemoveWatchlist;

public class RemoveWatchlistCommandHandler :
    IRequestHandler<RemoveWatchlistCommand, Result<Deleted>>
{
    private readonly IWatchListRepository _watchListRepository;
    private readonly IWatchlistRequestClient _requestClient;

    public RemoveWatchlistCommandHandler(
        IWatchListRepository watchListRepository, 
        IWatchlistRequestClient requestClient)
    {
        _watchListRepository = watchListRepository;
        _requestClient = requestClient;
    }
    
    public async Task<Result<Deleted>> Handle(
        RemoveWatchlistCommand command,
        CancellationToken cancellationToken)
    {
        if (!await _watchListRepository
                .WatchlistExistsByIdAsync(command.WatchlistId))
        {
            var notFoundException = new WatchlistNotFoundException();
            return new Result<Deleted>(notFoundException);
        }
        
        var userId = await _requestClient.GetUserIdFromToken(
            command.Token);
        
        if (!await IsWatchlistOwner(userId, command.WatchlistId))
        {
            var exception = new UnauthorizedAccessException(
                "You are not authorized to access this watchlist.");
            
            return new Result<Deleted>(exception);
        }

        await _watchListRepository
            .RemoveWatchListAsync(command.WatchlistId);

        return new Deleted();
    }
    
    private async Task<bool> IsWatchlistOwner(string userId, string watchlistId)
    {
        var dbWatchlist =  await _watchListRepository
            .GetWatchlistByIdAsync(watchlistId);
        
        return dbWatchlist.UserId == userId;
    }
}