using WatchlistService.Data.Repositories;
using WatchlistService.Common.Events;
using WatchlistService.Dtos.Requests;
using WatchlistService.Models;
using LanguageExt.Common;
using FluentValidation;
using MongoDB.Bson;
using WatchlistService.MessageBus;
using WatchlistService.MessageBus.Requests;
using WatchlistService.MessageBus.Responses;

namespace WatchlistService.Common.Services;

public class WatchlistServiceImp : IWatchlistService
{
    private readonly IValidator<CreateWatchlistRequest> _validator;
    private readonly IWatchListRepository _watchListRepository;
    private readonly Requestor _requestor;

    public WatchlistServiceImp(
        IValidator<CreateWatchlistRequest> validator, 
        IWatchListRepository watchListRepository, 
        Requestor requestor)
    {
        _validator = validator;
        _watchListRepository = watchListRepository;
        _requestor = requestor;
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

        DecodeTokenResponse response = await GetUserIdFromToken(token);
        
        Console.WriteLine($"UserId: {response.UserId}");
        
        var watchlist = new Watchlist
        {
            Id = ObjectId.GenerateNewId().ToString(),
            UserId = response.UserId,
            Name = "watchlist",
            MoviesId = request.MoviesId
        };
        
        await _watchListRepository.CreateWatchListAsync(watchlist);
        return watchlist;
    }

    private async Task<DecodeTokenResponse> GetUserIdFromToken(string jwt)
    {
        string[] token = jwt.Split();
        
        var decodeTokenRequest = new DecodeTokenRequest(
            token[1]);

        var response = await _requestor.SendRequest(decodeTokenRequest);

        return response;
    }
}