using WatchlistService.Common.Services;
using WatchlistService.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WatchlistService.Controllers;

[ApiController]
[Route("watchlist")]
public class WatchlistController : ApiController
{
    private readonly IWatchlistService _watchListService;
    public WatchlistController(IWatchlistService watchListService)
    {
        _watchListService = watchListService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateWatchlist(CreateWatchlistRequest request)
    {
        var result = await _watchListService.CreateWatchlist(request);

        return result.Match<IActionResult>(Ok, Problem);
    }

    [HttpGet("{watchlistId}")]
    public async Task<IActionResult> Get(string watchlistId)
    {
        return Ok();
    }
}