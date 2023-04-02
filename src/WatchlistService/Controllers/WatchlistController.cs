using Microsoft.AspNetCore.Authorization;
using WatchlistService.Common.Services;
using WatchlistService.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WatchlistService.Controllers;

[ApiController]
[Route("watchlist")]
[Authorize]
public class WatchlistController : ApiController
{
    private readonly IWatchlistService _watchListService;
    public WatchlistController(IWatchlistService watchListService)
    {
        _watchListService = watchListService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWatchlist(CreateWatchlistRequest request)
    {
        var result = await _watchListService.CreateWatchlist(request);
        return result.Match(Ok, Problem);
    }

    [HttpGet("{watchlistId}")]
    public async Task<IActionResult> Get(string watchlistId)
    {
        return Ok();
    }
}