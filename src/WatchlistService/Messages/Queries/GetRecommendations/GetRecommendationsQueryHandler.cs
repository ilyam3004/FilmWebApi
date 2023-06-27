using WatchlistService.Bus.Clients;
using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Responses;
using LanguageExt.Common;
using MediatR;

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
            var exception = new WatchlistNotFoundException();
            return new Result<List<RecommendationsResponse>>(exception);
        }
        
        var recommendationsResponse = new List<RecommendationsResponse>();
        foreach (var watchlist in watchlists)
        {
            int countOfMovies = MaxCountOfMoviesInWatchlist / watchlist.Movies.Count;
            var recommendations = await _watchlistRequestClient.GetWatchlistRecommendations(
                watchlist.Movies.Select(m => 
                    m.MovieId).ToList(), countOfMovies);

            recommendationsResponse.Add(
                new RecommendationsResponse
                {
                    WatchlistName = watchlist.Name,
                    Movies = recommendations
                });
        }
        
        return recommendationsResponse;
    }
}