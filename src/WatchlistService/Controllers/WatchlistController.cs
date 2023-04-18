using WatchlistService.Messages.Queries.GetUserWatchlists;
using WatchlistService.Messages.Commands.CreateWatchlist;
using WatchlistService.Messages.Commands.RemoveWatchlist;
using WatchlistService.Messages.Queries.GetWatchlist;
using WatchlistService.Messages.Commands.RemoveMovie;
using WatchlistService.Messages.Commands.AddMovie;
using Microsoft.AspNetCore.Authorization;
using WatchlistService.Dtos.Responses;
using WatchlistService.Dtos.Requests;
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

    [HttpPost]
    public async Task<IActionResult> CreateWatchlist(CreateWatchlistRequest request)
    {
        string token = HttpContext.Request.Headers["Authorization"]!;
        
        var command = new CreateWatchlistCommand(
            request.WatchlistName,
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
        var getWatchlistQuery = new GetWatchlistQuery(watchlistId);

        var result = await _sender.Send(getWatchlistQuery);
        
        return result.Match(Ok, Problem);
    }

    [HttpDelete("{watchlistId}")]
    public async Task<IActionResult> RemoveWatchlist(string watchlistId)
    {
        var command = new RemoveWatchlistCommand(
            watchlistId);
        
        var result = await _sender.Send(command);
        
        return result.Match(
            value => NoContent(), 
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> GetWatchlists()
    {
        string token = HttpContext.Request.Headers["Authorization"]!;
        var query = new GetWatchlistsQuery(token);

        var result = await _sender.Send(query);

        return result.Match(Ok, Problem);  
    }

    [HttpPost("{watchlistId}/movie/{movieId}")]
    public async Task<IActionResult> AddMovieToWatchlist(
        string watchlistId,
        int movieId)
    {
        var command = new AddMovieCommand(watchlistId, movieId);

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
        var command = new RemoveMovieCommand(watchlistId, movieId);
        
        var result = await _sender.Send(command);

        return result.Match(
            value => NoContent(),
            Problem);
    }
}