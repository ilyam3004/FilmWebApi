using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;
using WatchlistService.Models;
using WatchlistService.Bus;
using LanguageExt.Common;
using MongoDB.Bson;
using AutoMapper;
using MediatR;

namespace WatchlistService.Messages.Commands.CreateWatchlist;

public class CreateWatchlistCommandHandler : 
    IRequestHandler<CreateWatchlistCommand, Result<Watchlist>>
{
    private readonly IWatchListRepository _watchListRepository;
    private readonly IWatchlistRequestClient _requestClient;
    private readonly IMapper _mapper;

    public CreateWatchlistCommandHandler(
        IWatchListRepository watchListRepository, 
        IWatchlistRequestClient requestClient,
        IMapper mapper) 
    {
        _watchListRepository = watchListRepository;
        _requestClient = requestClient;
        _mapper = mapper;
    }
    
    public async Task<Result<Watchlist>> Handle(
        CreateWatchlistCommand command, 
        CancellationToken cancellationToken)
    {
        var userId = await _requestClient
            .GetUserIdFromToken(command.Token);

        if(await _watchListRepository
               .WatchlistExistsByNameAsync(userId, command.WatchlistName))
        {
            return new Result<Watchlist>(new DuplicateWatchlistException());
        }

        var watchlist = new Watchlist
        {
            Id = ObjectId.GenerateNewId().ToString(),
            UserId = userId,
            Name = command.WatchlistName,
            Movies = new List<WatchlistMovie>(),
            DateTimeOfCreating = DateTime.UtcNow,
        };

        await _watchListRepository.CreateWatchListAsync(watchlist);

        return watchlist;
    }
}