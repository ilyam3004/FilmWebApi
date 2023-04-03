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
        string token = HttpContext.Request.Headers["Authorization"]!;
        var result = await _watchListService.CreateWatchlist(request, token);
        return result.Match(Ok, Problem);
    }

    [HttpGet("{watchlistId}")]
    public async Task<IActionResult> Get(string watchlistId)
    {
        return Ok();
    }
}