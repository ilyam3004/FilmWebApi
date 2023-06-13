using WatchlistService.Messages.Queries.GetUserWatchlists;
using WatchlistService.Messages.Commands.CreateWatchlist;
using WatchlistService.Messages.Commands.RemoveWatchlist;
using WatchlistService.Messages.Queries.GetWatchlist;
using WatchlistService.Messages.Commands.RemoveMovie;
using WatchlistService.Messages.Commands.AddMovie;
using Microsoft.AspNetCore.Authorization;
using WatchlistService.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;
using TMDbLib.Objects.Movies;
using AutoMapper;
using MediatR;

namespace WatchlistService.Controllers;

[ApiController]
[Route("watchlists")]
[Authorize]
public class WatchlistController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;
    
    public WatchlistController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpPost("{watchlistName}")]
    public async Task<IActionResult> CreateWatchlist(string watchlistName)
    {
        string token = HttpContext.Request.Headers["Authorization"]!;
        
        var command = new CreateWatchlistCommand(
            watchlistName,
            token);

        var result = await _sender.Send(command);
        
        return result.Match(
            value => CreatedAtRoute("GetWatchlist", 
                new {watchlistId = value.Id}, 
                _mapper.Map<WatchlistResponse>((value, new List<Movie>()))), 
            Problem); 
    }

    [HttpGet("{watchlistId}", Name = "GetWatchlist")]
    public async Task<IActionResult> GetWatchlist(string watchlistId)
    {
        var token = HttpContext.Request.Headers["Authorization"]!;
        
        var getWatchlistQuery = new GetWatchlistQuery(
            watchlistId, token);

        var result = await _sender.Send(getWatchlistQuery);
        
        return result.Match(Ok, Problem);
    }

    [HttpDelete("{watchlistId}")]
    public async Task<IActionResult> RemoveWatchlist(string watchlistId)
    {
        string token = HttpContext.Request.Headers["Authorization"]!;
        var command = new RemoveWatchlistCommand(
            watchlistId, 
            token);
        
        var result = await _sender.Send(command);
        
        return result.Match(
            value => NoContent(), 
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> GetWatchlists()
    {
        string token = HttpContext.Request.Headers["Authorization"]!;
        var query = new GetUserWatchlistsQuery(token);

        var result = await _sender.Send(query);

        return Ok(result);
    }

    [HttpPost("{watchlistId}/movie/{movieId}")]
    public async Task<IActionResult> AddMovieToWatchlist(
        string watchlistId,
        int movieId)
    {
        var token = HttpContext.Request.Headers["Authorization"]!;
        
        var command = new AddMovieCommand(
            watchlistId, 
            movieId,
            token);

        var result = await _sender.Send(command);
        
        return result.Match(
            value => CreatedAtRoute("GetWatchlist", 
                new {watchlistId = value.Id}, 
                value), 
            Problem);
    }

    [HttpDelete("{watchlistId}/movie/{movieId}")]
    public async Task<IActionResult> RemoveFromWatchlist(string watchlistId, int movieId)
    {
        var token = HttpContext.Request.Headers["Authorization"]!;
        var command = new RemoveMovieCommand(watchlistId, token, movieId);
        
        var result = await _sender.Send(command);

        return result.Match(
            value => NoContent(),
            Problem);
    }
}