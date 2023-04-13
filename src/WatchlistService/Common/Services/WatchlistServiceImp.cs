using Watchlist = WatchlistService.Models.Watchlist;
using WatchlistService.Data.Repositories;
using WatchlistService.Common.Exceptions;
using WatchlistService.Dtos.Requests;
using WatchlistService.Dtos.Responses;
using Shared.Replies;
using Shared.Messages;
using LanguageExt.Common;
using FluentValidation;
using MassTransit;
using MongoDB.Bson;

namespace WatchlistService.Common.Services;

public class WatchlistServiceImp : IWatchlistService
{
    private readonly IValidator<CreateWatchlistRequest> _validator;
    private readonly IWatchListRepository _watchListRepository;
    private readonly IRequestClient<DecodeTokenMessage> _requestClient;

    public WatchlistServiceImp(
        IValidator<CreateWatchlistRequest> validator, 
        IWatchListRepository watchListRepository, 
        IRequestClient<DecodeTokenMessage> requestClient)
    {
        _validator = validator;
        _watchListRepository = watchListRepository;
        _requestClient = requestClient;
    }

    public async Task<Result<CreateWatchlistResponse>> CreateWatchlist(
        CreateWatchlistRequest request,
        string token)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var validationException = new ValidationException(
                validationResult.Errors);

            return new Result<CreateWatchlistResponse>(validationException);
        }

        var userId = await GetUserIdFromToken(token);

        if(await _watchListRepository
                .WatchlistExistsAsync(request.WatchlistName, userId))
        {
            return new Result<CreateWatchlistResponse>(new DuplicateWatchlistException(
                $"Watchlist with name {request.WatchlistName} already exists"));
        }

        var watchlist = new Watchlist
        {
            Id = ObjectId.GenerateNewId().ToString(),
            UserId = userId,
            Name = request.WatchlistName,
            MoviesId = request.MoviesId
        };

        await _watchListRepository.CreateWatchListAsync(watchlist);

        return new CreateWatchlistResponse
        {
            Id = watchlist.Id,
            Name = watchlist.Name
        };
    }

    public async Task<Result<List<Watchlist>>> GetWatchlists(string token)
    {
        string userId = await GetUserIdFromToken(token);

        return await _watchListRepository.GetWatchlistsAsync(userId);
    }

    public async Task<Result<Watchlist>> GetWatchlistByIdAsync(string watchlistId)
    {
        return await _watchListRepository.GetWatchlistByIdAsync(watchlistId);
    }

    private async Task<string> GetUserIdFromToken(string jwt)
    {
        string[] token = jwt.Split();
         
        var response = await _requestClient.GetResponse<DecodeTokenReply>(
                new DecodeTokenMessage
                {
                    Token = token[1]
                });
        
        return response.Message.UserId;
    }
}