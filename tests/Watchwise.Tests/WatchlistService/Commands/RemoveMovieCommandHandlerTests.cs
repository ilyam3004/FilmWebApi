using AutoFixture;
using AutoMapper;
using Moq;
using TMDbLib.Objects.Movies;
using WatchlistService.Bus;
using WatchlistService.Data.Repositories;
using WatchlistService.Messages.Commands.RemoveMovie;
using WatchlistService.Models;
using Watchwise.Tests.WatchlistService.Config;

namespace Watchwise.Tests.WatchlistService.Commands;

public class RemoveMovieCommandHandlerTests
{
    private readonly Mock<IWatchListRepository> _watchlistRepositoryMock = new();
    private readonly Mock<IWatchlistRequestClient> _requestClientMock = new();
    private readonly RemoveMovieCommandHandler _sut;
    private readonly Fixture _fixture;
    private readonly IMapper _mapper;
    
    public RemoveMovieCommandHandlerTests()
    {
        _fixture = new Fixture();
        _mapper = AutoMapperInitializer.ConfigureAutoMapper();
        _sut = new RemoveMovieCommandHandler(
            _watchlistRepositoryMock.Object,
            _mapper,
            _requestClientMock.Object);
    }
    
    [Fact]
    public async Task Handler_ShouldReturnWatchlistResponse()
    {
        //Arrange
        var command = _fixture.Create<RemoveMovieCommand>();

        var watchlist = _fixture.Create<Watchlist>();
        var watchlistMoviesCount = watchlist.Movies.Count;

        _watchlistRepositoryMock
            .Setup(x => x.WatchlistExistsByIdAsync(command.WatchlistId))
            .ReturnsAsync(true);

        _requestClientMock
            .Setup(x => x.GetUserIdFromToken(command.Token))
            .ReturnsAsync(watchlist.UserId);

        _watchlistRepositoryMock.Setup(x =>
                x.MovieExistsInWatchlistAsync(command.WatchlistId, command.MovieId))
            .ReturnsAsync(true);
        
        _watchlistRepositoryMock.Setup(x =>
                x.GetWatchlistByIdAsync(command.WatchlistId))
            .ReturnsAsync(watchlist);

        _requestClientMock.Setup(x =>
                x.GetMoviesData(It.IsAny<List<int>>()))
            .ReturnsAsync(GenerateMovieObjects(watchlistMoviesCount));

        //Act
        var response = await _sut.Handle(command, 
            CancellationToken.None);

        //Assert
        response.IfSucc(w =>
        {
            Assert.Equal(w.MoviesCount, watchlistMoviesCount - 1);
        });

        response.IfFail(e =>
        {
            Assert.True(false, "Should not return error");
        });
    }
    
    private List<Movie> GenerateMovieObjects(int count)
    {
        var movies = Enumerable.Range(0, count).Select(_ => new Movie()).ToList();

        return movies;
    }
}