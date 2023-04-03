using System.Collections;
using System.Security.Claims;
using WatchlistService.Data.Repositories;
using WatchlistService.Common.Events;
using WatchlistService.Dtos.Requests;
using WatchlistService.Models;
using LanguageExt.Common;
using FluentValidation;
using MongoDB.Bson;

namespace WatchlistService.Common.Services;

public class WatchlistServiceImp : IWatchlistService
{
    private readonly IValidator<CreateWatchlistRequest> _validator;
    private readonly IWatchListRepository _watchListRepository;
    private readonly IMessageBusProducer _messageBusProducer;

    public WatchlistServiceImp(
        IValidator<CreateWatchlistRequest> validator, 
        IWatchListRepository watchListRepository,
        IMessageBusProducer messageBusProducer)
    {
        _validator = validator;
        _watchListRepository = watchListRepository;
        _messageBusProducer = messageBusProducer;
    }

    public async Task<Result<Watchlist>> CreateWatchlist(
        CreateWatchlistRequest request,
        string token)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var validationException = new ValidationException(
                validationResult.Errors);

            return new Result<Watchlist>(validationException);
        }

        string userId = GetUserIdFromToken(token);
        
        Console.WriteLine($"UserId: {userId}");
        
        var watchlist = new Watchlist
        {
            Id = ObjectId.GenerateNewId().ToString(),
            UserId = Guid.NewGuid().ToString(),
            Name = "watchlist",
            MoviesId = request.MoviesId
        };
        
        await _watchListRepository.CreateWatchListAsync(watchlist);
        return watchlist;
    }

    private string GetUserIdFromToken(string jwt)
    {
        string[] token = jwt.Split();
        
        var decodeTokenRequest = new DecodeTokenRequest(
            token[1], 
            "decode-token-event");
        
        return _messageBusProducer
            .PublishDecodeTokenMessage(decodeTokenRequest);
    }
}