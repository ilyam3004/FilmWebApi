using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Responses;
using WatchlistService.Bus;
using LanguageExt.Common;
using AutoMapper;
using MediatR;
using TMDbLib.Objects.Movies;
using WatchlistService.Dtos;
using WatchlistService.Models;

namespace WatchlistService.Messages.Queries.GetUserWatchlists;

public class GetUserWatchlistsQueryHandler :
    IRequestHandler<GetUserWatchlistsQuery, Result<List<WatchlistResponse>>>
{
    private readonly IWatchListRepository _watchListRepository;
    private readonly IWatchlistRequestClient _requestClient;
    private readonly IMapper _mapper;
    
    public GetUserWatchlistsQueryHandler(
        IWatchListRepository watchListRepository, 
        IWatchlistRequestClient requestClient, 
        IMapper mapper)
    {
        _watchListRepository = watchListRepository;
        _requestClient = requestClient;
        _mapper = mapper;
    }

    public async Task<Result<List<WatchlistResponse>>> Handle(
        GetUserWatchlistsQuery query, 
        CancellationToken cancellationToken)
    {
        string userId = await _requestClient
            .GetUserIdFromToken(query.Token);

        var dbWatchlists = await _watchListRepository
            .GetWatchlistsAsync(userId);

        var watchlists = new List<WatchlistResponse>();

        foreach (var watchlist in dbWatchlists)
        {
            WatchlistResponse watchlistResponse = await GetWatchlistResponse(
                watchlist);

            watchlists.Add(watchlistResponse);
        }

        return watchlists;
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