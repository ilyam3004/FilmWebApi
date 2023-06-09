using AutoFixture;
using AutoMapper;
using Moq;
using TMDbLib.Objects.Movies;
using WatchlistService.Bus;
using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;
using WatchlistService.Messages.Commands.AddMovie;
using WatchlistService.Models;
using Watchwise.Tests.WatchlistService.Config;

namespace Watchwise.Tests.WatchlistService.Commands;

public class AddMovieCommandHandlerTests
{
    private readonly Mock<IWatchListRepository> _watchlistRepositoryMock = new();
    private readonly Mock<IWatchlistRequestClient> _requestClientMock = new();
    private readonly AddMovieCommandHandler _sut;
    private readonly IMapper _mapper;
    private readonly Fixture _fixture;

    public AddMovieCommandHandlerTests()
    {
        _fixture = new Fixture();
        _mapper = AutoMapperInitializer.ConfigureAutoMapper();
        _sut = new AddMovieCommandHandler(
            _watchlistRepositoryMock.Object,
            _requestClientMock.Object,
            _mapper);
    }

    [Fact]
    public async Task Handler_ShouldReturnWatchlistResponse()
    {
        //Arrange
        var command = _fixture.Create<AddMovieCommand>();

        var watchlist = _fixture.Create<Watchlist>();
        var watchlistMoviesCount = watchlist.Movies.Count;

        _watchlistRepositoryMock
            .Setup(x => x.WatchlistExistsByIdAsync(command.WatchlistId))
            .ReturnsAsync(true);

        _requestClientMock
            .Setup(x => x.GetUserIdFromToken(command.Token))
            .ReturnsAsync(watchlist.UserId);

        _watchlistRepositoryMock.Setup(x =>
                x.GetWatchlistByIdAsync(command.WatchlistId))
            .ReturnsAsync(watchlist);

        _watchlistRepositoryMock.Setup(x =>
                x.MovieExistsInWatchlistAsync(command.WatchlistId, command.MovieId))
            .ReturnsAsync(false);

        _requestClientMock.Setup(x =>
                x.GetMoviesData(It.IsAny<List<int>>()))
            .ReturnsAsync(GenerateMovieObjects(watchlistMoviesCount));

        //Act
        var response = await _sut.Handle(command, CancellationToken.None);

        //Assert
        response.IfSucc(w =>
        {
            Assert.Equal(w.MoviesCount, watchlist.Movies.Count);
            Assert.Equal(w.Name, watchlist.Name);
            Assert.Equal(w.DateTimeOfCreating, watchlist.DateTimeOfCreating);
        });

        response.IfFail(e => { Assert.True(false, "Should not return error"); });
    }
    
    [Fact]
    public async Task Handler_ShouldReturnException_WhenUserIsNotHaveAccessToTheWatchlist()
    {
        //Arrange
        var command = _fixture.Create<AddMovieCommand>();
        var watchlist = _fixture.Create<Watchlist>();
        var anotherUserId = Guid.NewGuid().ToString();
        var watchlistMoviesCount = watchlist.Movies.Count;

        _watchlistRepositoryMock
            .Setup(x => x.WatchlistExistsByIdAsync(command.WatchlistId))
            .ReturnsAsync(true);

        _requestClientMock
            .Setup(x => x.GetUserIdFromToken(command.Token))
            .ReturnsAsync(anotherUserId);

        _watchlistRepositoryMock.Setup(x =>
                x.GetWatchlistByIdAsync(command.WatchlistId))
            .ReturnsAsync(watchlist);

        //Act
        var response = await _sut.Handle(command, CancellationToken.None);

        //Assert
        response.IfSucc(w =>
        {
            Assert.True(false, "Should not return watchlist response");
        });

        response.IfFail(e =>
        {
            Assert.IsType<UnauthorizedAccessException>(e);
        });
    }
    
    [Fact]
    public async Task Handler_ShouldReturnException_WhenWatchlistNotExists()
    {
        //Arrange
        var command = _fixture.Create<AddMovieCommand>();

        _watchlistRepositoryMock
            .Setup(x => x.WatchlistExistsByIdAsync(command.WatchlistId))
            .ReturnsAsync(false);
        
        //Act
        var response = await _sut.Handle(command, CancellationToken.None);

        //Assert
        response.IfSucc(w =>
        {
            Assert.True(false, "Should not return watchlist response");
        });

        response.IfFail(e =>
        {
            Assert.IsType<WatchlistNotFoundException>(e);
        });
    }
    
    [Fact]
    public async Task Handler_ShouldReturnException_WhenMovieAlreadyExistsInThisWatchlist()
    {
        //Arrange
        var command = _fixture.Create<AddMovieCommand>();
        var watchlist = _fixture.Create<Watchlist>();

        _watchlistRepositoryMock
            .Setup(x => x.WatchlistExistsByIdAsync(command.WatchlistId))
            .ReturnsAsync(true);

        _requestClientMock
            .Setup(x => x.GetUserIdFromToken(command.Token))
            .ReturnsAsync(watchlist.UserId);

        _watchlistRepositoryMock.Setup(x =>
                x.GetWatchlistByIdAsync(command.WatchlistId))
            .ReturnsAsync(watchlist);

        _watchlistRepositoryMock.Setup(x =>
                x.MovieExistsInWatchlistAsync(command.WatchlistId, command.MovieId))
            .ReturnsAsync(true);

        //Act
        var response = await _sut.Handle(command, CancellationToken.None);

        //Assert
        response.IfSucc(w =>
        {
            Assert.True(false, "Should not return watchlist response");
        });

        response.IfFail(e =>
        {
            Assert.IsType<DuplicateMovieInWatchlistException>(e);
        });
    }

    private List<Movie> GenerateMovieObjects(int count)
    {
        var movies = Enumerable.Range(0, count).Select(_ => new Movie()).ToList();

        return movies;
    }
}