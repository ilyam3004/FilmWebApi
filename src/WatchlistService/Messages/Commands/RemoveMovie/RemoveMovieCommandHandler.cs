using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Responses;
using LanguageExt.Common;
using MediatR;

namespace WatchlistService.Messages.Commands.RemoveMovie;

public class RemoveMovieCommandHandler :
    IRequestHandler<RemoveMovieCommand, Result<Deleted>>
{
    private readonly IWatchListRepository _watchListRepository;
    
    public RemoveMovieCommandHandler(
        IWatchListRepository watchListRepository)
    {
        _watchListRepository = watchListRepository;
    }
    
    public async Task<Result<Deleted>> Handle(
        RemoveMovieCommand command, 
        CancellationToken cancellationToken)
    {
        if (!await _watchListRepository
                .WatchlistExistsByIdAsync(command.WatchlistId))
        {
            var notFoundException = new WatchlistNotFoundException();
            return new Result<Deleted>(notFoundException);
        }
        
        if (!await _watchListRepository
                .MovieExistsInWatchlistAsync(
                    command.WatchlistId, command.MovieId)) 
        {
            var notFoundException = new MovieNotFoundException();
            return new Result<Deleted>(notFoundException);
        }

        await _watchListRepository.RemoveMovieFromWatchlistAsync(
            command.WatchlistId, command.MovieId);

        return new Deleted();
    }
}