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

    public async Task<Result<Watchlist>> CreateWatchlist(CreateWatchlistRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var validationException = new ValidationException(
                validationResult.Errors);

            return new Result<Watchlist>(validationException);
        }
            
        var authRequest = new AuthUserRequest(
                request.Token, "Auth_User");
            
        string authResponse = _messageBusProducer.PublishAuthMessage(authRequest);
        Console.WriteLine($"Auth response: {authResponse}");
        
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
}