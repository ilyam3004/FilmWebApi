using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Responses;
using TMDbLib.Objects.Movies;
using WatchlistService.Dtos;
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

        if (!await IsWatchlistOwner(command.Token, command.WatchlistId))
        {
            var exception = new UnauthorizedAccessException(
                "You are not authorized to access this watchlist.");

            return new Result<WatchlistResponse>(exception);
        }

        if (!await _watchListRepository
                .MovieExistsInWatchlistAsync(command.WatchlistId, command.MovieId)) 
        {
            var notFoundException = new MovieNotFoundException();
            return new Result<WatchlistResponse>(notFoundException);
        }

        await _watchListRepository.RemoveMovieFromWatchlistAsync(
            command.WatchlistId, command.MovieId);

        return await GetUpdatedWatchlist(command.WatchlistId);
    }
    
    private async Task<bool> IsWatchlistOwner(string Token, string watchlistId)
    {
        var userId = await _requestClient.GetUserIdFromToken(Token);
        var dbWatchlist =  await _watchListRepository
            .GetWatchlistByIdAsync(watchlistId);
        
        return dbWatchlist.UserId == userId;
    }

    private async Task<Result<WatchlistResponse>> GetUpdatedWatchlist(string watchlistId)
    {
        var updatedWatchlist = await _watchListRepository
            .GetWatchlistByIdAsync(watchlistId);

        List<int> movieIds = updatedWatchlist.Movies.Select(x => x.MovieId)
            .ToList();

        List<DateTime> dateTimes = updatedWatchlist.Movies
            .Select(x => x.DateTimeOfAdding).ToList();

        List<Movie> movies = await _requestClient.GetMoviesData(movieIds);

        List<MovieResponse> movieResponses = movies.Zip(dateTimes, 
            (movie, dateTime) => 
                new MovieResponse 
                { 
                    Movie = movie, 
                    DateTimeOfAdding = dateTime
                }).ToList();

        return _mapper.Map<WatchlistResponse>((updatedWatchlist, movieResponses)); 
    }
}