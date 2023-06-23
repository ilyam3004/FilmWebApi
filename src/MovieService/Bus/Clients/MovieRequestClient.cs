using MassTransit;
using Shared.Messages;
using Shared.Models;
using Shared.Replies;

namespace MovieService.Bus.Clients;

public class MovieRequestClient : IMovieRequestClient
{
    private readonly IRequestClient<DecodeTokenMessage> _decodeTokenMessage;
    private readonly IRequestClient<UserWatchlistsMessage> _watchlistsMessage;
    
    public MovieRequestClient(
        IRequestClient<DecodeTokenMessage> decodeTokenMessage, 
        IRequestClient<UserWatchlistsMessage> watchlistsMessage)
    {
        _decodeTokenMessage = decodeTokenMessage;
        _watchlistsMessage = watchlistsMessage;
    }
    public async Task<List<Watchlist>> GetWatchlists(string userId)
    {
        var response = await _decodeTokenMessage
            .GetResponse<UserWatchlistsReply>(
                new UserWatchlistsMessage()
                {
                    UserId = userId
                });

        return response.Message.Watchlists;
    }

    public async Task<string> GetUserIdFromToken(string jwt)
    {
        string[] token = jwt.Split();

        var response = await _decodeTokenMessage.GetResponse<DecodeTokenReply>(
            new DecodeTokenMessage
            {
                Token = token[1]
            });

        return response.Message.UserId;
    }
}