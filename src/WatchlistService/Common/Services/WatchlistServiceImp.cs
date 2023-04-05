using WatchlistService.MessageBus.Requests;
using WatchlistService.MessageBus.Responses;
using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Requests;
using WatchlistService.Models;
using LanguageExt.Common;
using FluentValidation;
using MassTransit;
using MongoDB.Bson;

namespace WatchlistService.Common.Services;

public class WatchlistServiceImp : IWatchlistService
{
    private readonly IValidator<CreateWatchlistRequest> _validator;
    private readonly IWatchListRepository _watchListRepository;
    private readonly IRequestClient<DecodeTokenRequest> _requestClient;

    public WatchlistServiceImp(
        IValidator<CreateWatchlistRequest> validator, 
        IWatchListRepository watchListRepository,
        IRequestClient<DecodeTokenRequest> requestClient)
    {
        _validator = validator;
        _watchListRepository = watchListRepository;
        _requestClient = requestClient;
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
        
        var request = new DecodeTokenRequest
        {
            Token = token[1]
        };
        Console.WriteLine("Sending request to decode token");
        var response = await _requestClient
            .GetResponse<DecodeTokenResponse>(request);
        
        return response.Message;
    }
}