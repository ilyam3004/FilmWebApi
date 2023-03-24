using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WatchlistService.Dtos.Requests;
using WatchlistService.Models;
using WatchlistService.Services;

namespace WatchlistService.Controllers;

[Route("watchlist")]
[ApiController]
public class WatchlistController : ControllerBase
{
    private readonly WatchlistsService _watchlistsService;

    public WatchlistController(WatchlistsService watchlistsService)
    {
        _watchlistsService = watchlistsService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateWatchlist(CreateWatchlistRequest request)
    {
        var watchlist = new Watchlist
        {
            WatchlistId = ObjectId.GenerateNewId().ToString(),
            Name = request.Name,
            UserId = request.UserId.ToString()
        };

        await _watchlistsService.Create(watchlist);

        return Ok(watchlist);
    }

    [HttpGet("{watchlistId}")]
    public async Task<IActionResult> Get(string watchlistId)
    {
        var watchlist = await _watchlistsService.GetWatchlist(watchlistId);

        return Ok(watchlist);
    }
}