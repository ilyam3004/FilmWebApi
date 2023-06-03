using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Responses;
using WatchlistService.Bus;
using LanguageExt.Common;
using Shared.Messages;
using MassTransit;
using AutoMapper;
using MediatR;

namespace WatchlistService.Messages.Commands.AddMovie;

public class AddMovieCommandHandler :
    IRequestHandler<AddMovieCommand, Result<WatchlistResponse>>
{
    private readonly IWatchListRepository _watchListRepository;
    private readonly IWatchlistRequestClient _requestClient;
    private readonly IMapper _mapper;

    public AddMovieCommandHandler(
        IWatchListRepository watchListRepository, 
        IRequestClient<MoviesDataMessage> requestClient,
        IWatchlistRequestClient requestClient1, 
        IMapper mapper)
    {
        _watchListRepository = watchListRepository;
        _mapper = mapper;
        _requestClient = requestClient1;
    }
    
    public async Task<Result<WatchlistResponse>> Handle(
        AddMovieCommand command, 
        CancellationToken cancellationToken)
    {
        if (!await _watchListRepository
                .WatchlistExistsByIdAsync(command.WatchlistId))
        {
            var notFoundException = new WatchlistNotFoundException();
            return new Result<WatchlistResponse>(notFoundException);
        }
        
        if (!await IsWatchlistOwner(command.Token, command.WatchlistId))
        {
            var exception = new UnauthorizedAccessException(
                "You are not authorized to access this watchlist.");
            
            return new Result<WatchlistResponse>(exception);
        }
        
        if (await _watchListRepository
                .MovieExistsInWatchlistAsync(command.WatchlistId, command.MovieId))
        {
            return new Result<WatchlistResponse>(new DuplicateMovieInWatchlistException());
        }

        DateTime dateTimeOfAdding = DateTime.UtcNow;
        
        await _watchListRepository.AddMovieToWatchlistAsync(
            command.WatchlistId, command.MovieId, dateTimeOfAdding);
        
        var updatedWatchlist = await _watchListRepository
            .GetWatchlistByIdAsync(command.WatchlistId);
        
        var moviesData = await _requestClient.
            GetMoviesData(updatedWatchlist.Movies);

        return _mapper.Map<WatchlistResponse>((updatedWatchlist, moviesData));
    }
    
    private async Task<bool> IsWatchlistOwner(string Token, string watchlistId)
    {
        var userId = await _requestClient.GetUserIdFromToken(Token);
        var dbWatchlist =  await _watchListRepository
            .GetWatchlistByIdAsync(watchlistId);
        
        return dbWatchlist.UserId == userId;
    }
}