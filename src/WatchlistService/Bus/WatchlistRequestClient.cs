using TMDbLib.Objects.Movies;
using Shared.Messages;
using Shared.Replies;
using MassTransit;

namespace WatchlistService.Bus;

public class WatchlistRequestClient : IWatchlistRequestClient
{
    private readonly IRequestClient<DecodeTokenMessage> _decodeTokenRequestClient;
    private readonly IRequestClient<MoviesDataMessage> _movieDataRequestClient;

    public WatchlistRequestClient(
        IRequestClient<DecodeTokenMessage> decodeTokenRequestClient, 
        IRequestClient<MoviesDataMessage> movieDataRequestClient)
    {
        _decodeTokenRequestClient = decodeTokenRequestClient;
        _movieDataRequestClient = movieDataRequestClient;
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
}