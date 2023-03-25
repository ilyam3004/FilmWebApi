using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using WatchlistService.Models;
using MongoDB.Bson;

namespace WatchlistService.Controllers;

[Route("watchlist")]
[ApiController]
public class WatchlistController : ControllerBase
{
    private readonly IWatchListRepository _watchListRepository;
    public WatchlistController(IWatchListRepository watchListRepository)
    {
        _watchListRepository = watchListRepository;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateWatchlist(CreateWatchlistRequest request)
    {
        var watchlist = new Watchlist
        {
            WatchlistId = ObjectId.GenerateNewId().ToString(),
            WatchlistName = request.WatchlistName,
            UserId = request.Token
        };

        await _watchListRepository.CreateWatchListAsync(watchlist);

        return Ok(watchlist);
    }

    [HttpGet("{watchlistId}")]
    public async Task<IActionResult> Get(string watchlistId)
    {
        var watchlist = await _watchListRepository
            .GetWatchListsAsync(watchlistId);
        
        return Ok(watchlist);
    }
}