using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Responses;
using WatchlistService.Models;
using TMDbLib.Objects.Movies;
using WatchlistService.Dtos;
using WatchlistService.Bus;
using LanguageExt.Common;
using AutoMapper;
using MediatR;
using WatchlistService.Bus.Clients;

namespace WatchlistService.Messages.Queries.GetWatchlist;

public class GetWatchlistQueryHandler
    : IRequestHandler<GetWatchlistQuery, Result<WatchlistResponse>>
{
    private readonly IWatchListRepository _watchListRepository;
    private readonly IWatchlistRequestClient _requestClient;
    private readonly IMapper _mapper;
    
    public GetWatchlistQueryHandler(
        IWatchListRepository watchListRepository, 
        IWatchlistRequestClient requestClient,
        IMapper mapper)
    {
        _watchListRepository = watchListRepository;
        _requestClient = requestClient;
        _mapper = mapper;
    }
    
    public async Task<Result<WatchlistResponse>> Handle(
        GetWatchlistQuery query, CancellationToken cancellationToken)
    {
        if(!await _watchListRepository
               .WatchlistExistsByIdAsync(query.WatchlistId))
        {
            var notFoundException = new WatchlistNotFoundException();
            return new Result<WatchlistResponse>(notFoundException);
        }
        
        var dbWatchlist =  await _watchListRepository
            .GetWatchlistByIdAsync(query.WatchlistId);
        
        var userId = await _requestClient.GetUserIdFromToken(
            query.Token);

        if (dbWatchlist.UserId != userId)
        {
            var exception = new UnauthorizedAccessException(
                "You are not authorized to access this watchlist.");
            
            return new Result<WatchlistResponse>(exception);
        }

        WatchlistResponse watchlistResponse = await GetWatchlistResponse(
                dbWatchlist);

        return watchlistResponse;
    }

    private async Task<WatchlistResponse> GetWatchlistResponse(
        Watchlist watchlist) 
    {
        List<int> movieIds = watchlist.Movies.Select(x => x.MovieId)
            .ToList();

        List<DateTime> dateTimes = watchlist.Movies
            .Select(x => x.DateTimeOfAdding).ToList();

        List<Movie> movies = await _requestClient.GetMoviesData(movieIds);

        List<MovieResponse> movieResponses = movies.Zip(dateTimes, 
            (movie, dateTime) => 
            new MovieResponse 
            { 
                Movie = movie, 
                DateTimeOfAdding = dateTime
            }).ToList();

        return _mapper.Map<WatchlistResponse>((watchlist, movieResponses));
    } 
}