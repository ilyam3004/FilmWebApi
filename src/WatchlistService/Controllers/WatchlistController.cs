using MassTransit;
using WatchlistService.Common.Services;
using WatchlistService.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using Watchwise.Shared.Messages;
using Watchwise.Shared.Responses;

namespace WatchlistService.Controllers;

[ApiController]
[Route("watchlist")]
//[Authorize]
public class WatchlistController : ApiController
{
    private readonly IWatchlistService _watchListService;
    private readonly IRequestClient<DecodeTokenMessage> _requestClient;
    public WatchlistController(
        IWatchlistService watchListService,
        IPublishEndpoint publishEndpoint, IRequestClient<DecodeTokenMessage> requestClient)
    {
        _watchListService = watchListService;
        _requestClient = requestClient;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWatchlist(CreateWatchlistRequest request)
    {
        string token = HttpContext.Request.Headers["Authorization"]!;
        var result = await _watchListService.CreateWatchlist(request, token);
        return result.Match(Ok, Problem); 
    }

    [HttpPost("rabbitmq")]
    public async Task<IActionResult> CreateWatchlistRabbitMq()
    {
        string token = HttpContext.Request.Headers["Authorization"]!;
        Console.WriteLine("Sending request...");

        var result = await _requestClient
            .GetResponse<DecodeTokenMessageResponse>(new DecodeTokenMessage
            {
                Token = token
            });

        return Ok(result.Message);
    }
    
    [HttpGet("{watchlistId}")]
    public async Task<IActionResult> Get(string watchlistId)
    {
        return Ok();
    }
}