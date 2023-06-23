using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Moq;
using TMDbLib.Objects.Movies;
using WatchlistService.Bus;
using WatchlistService.Bus.Clients;
using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;
using WatchlistService.Messages.Queries.GetWatchlist;
using WatchlistService.Models;
using Watchwise.Tests.WatchlistService.Config;

namespace Watchwise.Tests.WatchlistService.Queries;

public class GetWatchlistQueryHandlerTests
{
    private readonly GetWatchlistQueryHandler _sut;
    private readonly Mock<IWatchListRepository> _watchlistRepositoryMock = new();
    private readonly Mock<IWatchlistRequestClient> _requestClientMock = new();
    private readonly Fixture _fixture;
    private readonly IMapper _mapper;

    public GetWatchlistQueryHandlerTests()
    {
        _fixture = new Fixture();
        _mapper = AutoMapperInitializer.ConfigureAutoMapper();
        _sut = new GetWatchlistQueryHandler(
            _watchlistRepositoryMock.Object,
            _requestClientMock.Object,
            _mapper);
    }

    [Fact]
    public async Task Handler_ShouldReturnWatchlistResponse()
    {
        //Arrange
        var query = _fixture.Create<GetWatchlistQuery>();
        var watchlist = _fixture.Create<Watchlist>();

        _watchlistRepositoryMock.Setup(x =>
                x.WatchlistExistsByIdAsync(query.WatchlistId))
            .ReturnsAsync(true);

        _watchlistRepositoryMock.Setup(x =>
                x.GetWatchlistByIdAsync(query.WatchlistId))
            .ReturnsAsync(watchlist);

        _requestClientMock.Setup(x => 
            x.GetUserIdFromToken(query.Token))
            .ReturnsAsync(watchlist.UserId);

        _requestClientMock.Setup(x => 
            x.GetMoviesData(It.IsAny<List<int>>()))
            .ReturnsAsync(GenerateMovieObjects(watchlist.Movies.Count));

        //Act
        var response = await _sut.Handle(query, CancellationToken.None);

        //Assert
        response.IfSucc(w =>
        {
            Assert.Equal(w.Movies.Count, watchlist.Movies.Count());
            Assert.Equal(w.Name, watchlist.Name);
            Assert.Equal(w.DateTimeOfCreating, watchlist.DateTimeOfCreating);
        });
        
        response.IfFail(f => 
            Assert.True(false, "Should not return an exception"));
    }
    
    [Fact]
    public async Task Handler_ShouldReturnException_WhenWatchlistNotExists()
    {
        //Arrange
        var query = _fixture.Create<GetWatchlistQuery>();

        _watchlistRepositoryMock.Setup(x =>
                x.WatchlistExistsByIdAsync(query.WatchlistId))
            .ReturnsAsync(false);

        //Act
        var response = await _sut.Handle(query, CancellationToken.None);

        //Assert
        response.IfSucc(w => 
            Assert.True(false, "Should not return the value"));
        
        response.IfFail(e => 
            Assert.IsType<WatchlistNotFoundException>(e));
    }
    
    [Fact]
    public async Task Handler_ShouldReturnException_WhenUserHaveNotAccessToTheWatchlist()
    {
        //Arrange
        var query = _fixture.Create<GetWatchlistQuery>();
        var watchlist = _fixture.Create<Watchlist>();

        _watchlistRepositoryMock.Setup(x =>
                x.WatchlistExistsByIdAsync(query.WatchlistId))
            .ReturnsAsync(true);

        _watchlistRepositoryMock.Setup(x =>
                x.GetWatchlistByIdAsync(query.WatchlistId))
            .ReturnsAsync(watchlist);

        _requestClientMock.Setup(x =>
                x.GetUserIdFromToken(query.Token))
            .ReturnsAsync(Guid.NewGuid().ToString);
        
        //Act
        var response = await _sut.Handle(query, CancellationToken.None);

        //Assert
        response.IfSucc(w => Assert
            .True(false, "Should not return the value"));
        
        response.IfFail(e => 
            Assert.IsType<UnauthorizedAccessException>(e));
    }
    
    private List<Movie> GenerateMovieObjects(int count)
    {
        var movies = Enumerable.Range(0, count).Select(_ => new Movie()).ToList();

        return movies;
    }
}