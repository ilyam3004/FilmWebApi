using UserService.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using UserService.Dtos.Responses;
using UserService.Controllers;
using UserService.Services;
using LanguageExt.Common;
using AutoFixture;
using AutoMapper;
using Moq;

namespace UserServiceTests.Controllers;

public class UserControllerTests
{
    private readonly UserController _sut;
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();
    private readonly Fixture _fixture;

    public UserControllerTests()
    {
        _sut = new UserController(_userServiceMock.Object, _mapperMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task Controller_ShouldReturnvalidatio()
    {
        //Arrange
        var request = _fixture.Create<RegisterRequest>();
        var response = _fixture.Create<RegisterResponse>();
        
        _userServiceMock.Setup(
                u => u.Register(request))
            .ReturnsAsync(new Result<RegisterResponse>(response));

        //Act
        var result = await _sut.Register(request);
        
        //Assert
        Assert.Equal(200, ((OkObjectResult)result).StatusCode);
    }
}