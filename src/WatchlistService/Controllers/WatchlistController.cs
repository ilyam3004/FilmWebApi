using Microsoft.AspNetCore.Authorization;
using WatchlistService.Common.Services;
using WatchlistService.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WatchlistService.Controllers;

[ApiController]
[Route("watchlists")]
[Authorize]
public class WatchlistController : ApiController
{
    private readonly IWatchlistService _watchListService;
    public WatchlistController(
        IWatchlistService watchListService)
    {
        _watchListService = watchListService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWatchlist(CreateWatchlistRequest request)
    {
        string token = HttpContext.Request.Headers["Authorization"]!;
        var result = await _watchListService.CreateWatchlist(request, token);
        return result.Match(
            watchlist => Created($"watchlists/{watchlist.Id}", watchlist), 
            Problem); 
    }
    
    [HttpGet("{watchlistId}")]
    public async Task<IActionResult> GetWatchlist(string watchlistId)
    {
        var result = await _watchListService
            .GetWatchlistByIdAsync(watchlistId);

        return result.Match(Ok, Problem);
    }
    
    [HttpDelete("{watchlistId}")]
    public async Task<IActionResult> RemoveWatchlist(string watchlistId)
    {
        await _watchListService.RemoveWatchlistAsync(watchlistId);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetWatchlists()
    {
        string token = HttpContext.Request.Headers["Authorization"]!;

        var result = await _watchListService.GetWatchlists(token);

        return result.Match(Ok, Problem);  
    }

    [HttpPost("{watchlistId}/movie/{movieId}")]
    public async Task<IActionResult> AddMovieToWatchlist(
        string watchlistId,
        int movieId)
    {
        var result = await _watchListService
            .AddMovieToWatchlist(watchlistId, movieId);
        
        return result.Match(Ok, Problem);
    }

    [HttpDelete("{watchlistId}/movie/{movieId}")]
    public async Task<IActionResult> RemoveFromWatchlist(string watchlistId, int movieId)
    {
        await _watchListService.RemoveMovieFromWatchlistAsync(watchlistId, movieId);
        return Ok();
    }
}