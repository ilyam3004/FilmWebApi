using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Requests;
using WatchlistService.Models;
using LanguageExt.Common;
using FluentValidation;
using MongoDB.Bson;
using WatchlistService.SyncDataServices;

namespace WatchlistService.Common.Services;

public class WatchlistServiceImp : IWatchlistService
{
    private readonly IValidator<CreateWatchlistRequest> _validator;
    private readonly IWatchListRepository _watchListRepository;
    private readonly IMessageBusClient _messageBusClient;
    
    public WatchlistServiceImp(
        IValidator<CreateWatchlistRequest> validator, 
        IWatchListRepository watchListRepository,
        IMessageBusClient messageBusClient)
    {
        _validator = validator;
        _watchListRepository = watchListRepository;
        _messageBusClient = messageBusClient;
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

        try
        {
            var authRequest = new AuthUserRequest(
                request.Token, "Auth_User");
            
            _messageBusClient.PublishAuthUser(authRequest);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

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