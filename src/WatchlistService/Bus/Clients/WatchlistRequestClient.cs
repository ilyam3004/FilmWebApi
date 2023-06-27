using MassTransit;
using Shared.Messages;
using Shared.Replies;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;

namespace WatchlistService.Bus.Clients;

public class WatchlistRequestClient : IWatchlistRequestClient
{
    private readonly IRequestClient<DecodeTokenMessage> _decodeTokenRequestClient;
    private readonly IRequestClient<MoviesDataMessage> _movieDataRequestClient;
    private readonly IRequestClient<WatchlistRecommendationsMessage> _watchlistRecommendationsRequestClient;

    public WatchlistRequestClient(
        IRequestClient<DecodeTokenMessage> decodeTokenRequestClient, 
        IRequestClient<MoviesDataMessage> movieDataRequestClient, 
        IRequestClient<WatchlistRecommendationsMessage> watchlistRecommendationsRequestClient)
    {
        _decodeTokenRequestClient = decodeTokenRequestClient;
        _movieDataRequestClient = movieDataRequestClient;
        _watchlistRecommendationsRequestClient = watchlistRecommendationsRequestClient;
    }

    public async Task<string> GetUserIdFromToken(string jwt)
    {
        string[] token = jwt.Split();

        var response = await _decodeTokenRequestClient
            .GetResponse<DecodeTokenReply>(
                new DecodeTokenMessage
                {
                    Token = token[1]
                });

        return response.Message.UserId;
    }

    public async Task<List<Movie>> GetMoviesData(List<int> moviesId)
    {
        var response = await _movieDataRequestClient
            .GetResponse<MoviesDataReply>(new MoviesDataMessage
            {
                MoviesId = moviesId
            });

        return response.Message.Movies;
    }

    public async Task<List<SearchMovie>> GetWatchlistRecommendations(
        List<int> moviesId, int moviesCount)
    {
        var response = await _watchlistRecommendationsRequestClient
            .GetResponse<WatchlistRecommendationsReply>(
                new WatchlistRecommendationsMessage
            {
                MoviesId = moviesId,
                MoviesCount = moviesCount
            });

        return response.Message.Movies;
    }
}