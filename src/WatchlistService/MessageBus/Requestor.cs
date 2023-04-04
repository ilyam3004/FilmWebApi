using MassTransit;
using WatchlistService.MessageBus.Requests;
using WatchlistService.MessageBus.Responses;

namespace WatchlistService.MessageBus;

public class Requestor
{
    private readonly IRequestClient<DecodeTokenRequest> _requestClient;

    public Requestor(IRequestClient<DecodeTokenRequest> requestClient)
    {
        _requestClient = requestClient;
    }

    public async Task<DecodeTokenResponse> SendRequest(DecodeTokenRequest request)
    {
        Console.WriteLine("Sending request...");
        var response = await _requestClient.GetResponse<DecodeTokenResponse>(request);
        return response.Message;
    }
}