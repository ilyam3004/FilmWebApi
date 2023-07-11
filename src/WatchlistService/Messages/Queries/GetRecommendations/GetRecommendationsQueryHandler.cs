using WatchlistService.Bus.Clients;
using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Responses;
using LanguageExt.Common;
using MediatR;
using TMDbLib.Objects.Search;

namespace WatchlistService.Messages.Queries.GetRecommendations;

public class GetRecommendationsQueryHandler
    : IRequestHandler<GetRecommendationsQuery, Result<List<RecommendationsResponse>>>
{
    private readonly IWatchListRepository _watchListRepository;
    private readonly IWatchlistRequestClient _watchlistRequestClient;
    private const int MaxCountOfMoviesInWatchlist = 20;
    
    public GetRecommendationsQueryHandler(IWatchListRepository watchListRepository, 
        IWatchlistRequestClient watchlistRequestClient)
    {
        _watchListRepository = watchListRepository;
        _watchlistRequestClient = watchlistRequestClient;
    }
    
    public async Task<Result<List<RecommendationsResponse>>> Handle(
        GetRecommendationsQuery query, 
        CancellationToken cancellationToken)
    {
        var userId = await _watchlistRequestClient
            .GetUserIdFromToken(query.Token);
        
        // TODO: add unauthorized exception in shared
        var watchlists = await _watchListRepository
            .GetWatchlistsAsync(userId);
        
        if (watchlists.Count == 0)
        {
            var exception = new WatchlistNotFoundException("Watchlists not found");
            return new Result<List<RecommendationsResponse>>(exception);
        }

        var watchlistsRecommendations = new List<RecommendationsResponse>();
        foreach (var watchlist in watchlists)
        {
            if (watchlist.Movies.Count == 0) 
            {
                AddEmptyMovieRecommendations(ref watchlistsRecommendations, watchlist.Name);
                continue;
            }

            int countOfMovies = MaxCountOfMoviesInWatchlist / watchlist.Movies.Count;
            var recommendations = await _watchlistRequestClient.GetWatchlistRecommendations(
                watchlist.Movies.Select(m => 
                    m.MovieId).ToList(), countOfMovies);

            watchlistsRecommendations.Add(
                new RecommendationsResponse
                {
                    WatchlistName = watchlist.Name,
                    Movies = recommendations
                });
        }
        
        return watchlistsRecommendations;
    }

    private void AddEmptyMovieRecommendations(
        ref List<RecommendationsResponse> watchlistsRecommendations, 
        string watchlistName)
    {
        watchlistsRecommendations.Add(new RecommendationsResponse
        {
            WatchlistName = watchlistName,
            Movies = new List<SearchMovie>()
        });
    }
}