using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Responses;
using WatchlistService.Bus;
using LanguageExt.Common;
using AutoMapper;
using MediatR;

namespace WatchlistService.Messages.Commands.RemoveMovie;

public class RemoveMovieCommandHandler :
    IRequestHandler<RemoveMovieCommand, Result<WatchlistResponse>>
{
    private readonly IWatchListRepository _watchListRepository;
    private readonly IMapper _mapper;
    private readonly IWatchlistRequestClient _requestClient;

    public RemoveMovieCommandHandler(
        IWatchListRepository watchListRepository, 
        IMapper mapper, 
        IWatchlistRequestClient requestClient)
    {
        _watchListRepository = watchListRepository;
        _mapper = mapper;
        _requestClient = requestClient;
    }
    
    public async Task<Result<WatchlistResponse>> Handle(
        RemoveMovieCommand command, 
        CancellationToken cancellationToken)
    {
        if (!await _watchListRepository
                .WatchlistExistsByIdAsync(command.WatchlistId))
        {
            var notFoundException = new WatchlistNotFoundException();
            return new Result<WatchlistResponse>(notFoundException);
        }
        
        if (!await _watchListRepository
                .MovieExistsInWatchlistAsync(
                    command.WatchlistId, command.MovieId)) 
        {
            var notFoundException = new MovieNotFoundException();
            return new Result<WatchlistResponse>(notFoundException);
        }

        await _watchListRepository.RemoveMovieFromWatchlistAsync(
            command.WatchlistId, command.MovieId);
        

        return await GetUpdatedWatchlist(command.WatchlistId);
    }

    private async Task<Result<WatchlistResponse>> GetUpdatedWatchlist(
        string watchlistId)
    {
        var updatedWatchlist = await _watchListRepository
            .GetWatchlistByIdAsync(watchlistId);
        
        var moviesData = await _requestClient.
            GetMoviesData(updatedWatchlist.MoviesId);

        return _mapper.Map<WatchlistResponse>((updatedWatchlist, moviesData)); 
    }
}