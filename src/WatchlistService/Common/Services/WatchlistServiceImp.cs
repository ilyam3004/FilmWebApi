using FluentValidation;
using LanguageExt.Common;
using MongoDB.Bson;
using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Requests;
using WatchlistService.Models;

namespace WatchlistService.Common.Services;

public class WatchlistServiceImp : IWatchlistService
{
    private readonly IValidator<CreateWatchlistRequest> _validator;
    private readonly IWatchListRepository _watchListRepository;
    public WatchlistServiceImp(
        IValidator<CreateWatchlistRequest> validator, 
        IWatchListRepository watchListRepository)
    {
        _validator = validator;
        _watchListRepository = watchListRepository;
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