using WatchlistService.Dtos.Responses;
using LanguageExt.Common;
using MediatR;
using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;

namespace WatchlistService.Messages.Commands.RemoveWatchlist;

public class RemoveWatchlistCommandHandler :
    IRequestHandler<RemoveWatchlistCommand, Result<Deleted>>
{
    private readonly IWatchListRepository _watchListRepository; 
        
    public RemoveWatchlistCommandHandler(
        IWatchListRepository watchListRepository)
    {
        _watchListRepository = watchListRepository;
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
        
        await _watchListRepository
            .RemoveWatchListAsync(command.WatchlistId);

        return new Deleted();
    }
}