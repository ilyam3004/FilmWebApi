using AutoFixture;
using Moq;
using WatchlistService.Bus;
using WatchlistService.Bus.Clients;
using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Responses;
using WatchlistService.Messages.Commands.RemoveWatchlist;
using WatchlistService.Models;

namespace Watchwise.Tests.WatchlistService.Commands;

public class RemoveWatchlistCommandHandlerTests
{
    private readonly RemoveWatchlistCommandHandler _sut;
    private readonly Mock<IWatchListRepository> _watchlistRepositoryMock = new();
    private readonly Mock<IWatchlistRequestClient> _requestClientMock = new();
    private readonly Fixture _fixture;
    
    public RemoveWatchlistCommandHandlerTests()
    {
        _fixture = new Fixture();
        _sut = new RemoveWatchlistCommandHandler(
            _watchlistRepositoryMock.Object, 
            _requestClientMock.Object);
    }

    [Fact]
    public async Task Handler_ShouldReturnDeleted()
    {
        //Arrange
        var command = _fixture.Create<RemoveWatchlistCommand>();
        var userId = Guid.NewGuid().ToString();
        var watchlist = _fixture.Build<Watchlist>()
            .With(x => x.UserId, userId).Create();

        _watchlistRepositoryMock.Setup(x =>
            x.WatchlistExistsByIdAsync(command.WatchlistId)).ReturnsAsync(true);

        _requestClientMock.Setup(x =>
            x.GetUserIdFromToken(command.Token)).ReturnsAsync(userId);

        _watchlistRepositoryMock.Setup(x =>
            x.GetWatchlistByIdAsync(command.WatchlistId)).ReturnsAsync(watchlist);

        //Act
        var response = await _sut.Handle(command, CancellationToken.None);
        
        //Assert
        response.IfSucc(r =>
        {
            Assert.IsType<Deleted>(r);
        });
        
        response.IfFail(e =>
        {
            Assert.True(false, "Should not return error");
        });
    }
    
    [Fact]
    public async Task Handler_ShouldReturnException_WhenUserHaveNotAccessToTheWatchlist()
    {
        //Arrange
        var command = _fixture.Create<RemoveWatchlistCommand>();
        var watchlist = _fixture.Create<Watchlist>();

        _watchlistRepositoryMock.Setup(x =>
            x.WatchlistExistsByIdAsync(command.WatchlistId)).ReturnsAsync(true);

        _requestClientMock.Setup(x =>
            x.GetUserIdFromToken(command.Token)).ReturnsAsync(Guid.NewGuid().ToString);

        _watchlistRepositoryMock.Setup(x =>
            x.GetWatchlistByIdAsync(command.WatchlistId)).ReturnsAsync(watchlist);

        //Act
        var response = await _sut.Handle(command, CancellationToken.None);
        
        //Assert
        response.IfSucc(r =>
        {
            Assert.True(false, "Should not return value");
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
        var command = _fixture.Create<RemoveWatchlistCommand>();

        _watchlistRepositoryMock.Setup(x =>
            x.WatchlistExistsByIdAsync(command.WatchlistId)).ReturnsAsync(false);

        //Act
        var response = await _sut.Handle(command, CancellationToken.None);
        
        //Assert
        response.IfSucc(r =>
        {
            Assert.True(false, "Should not return value");
        });
        
        response.IfFail(e =>
        {
            Assert.IsType<WatchlistNotFoundException>(e);
        });
    }
}