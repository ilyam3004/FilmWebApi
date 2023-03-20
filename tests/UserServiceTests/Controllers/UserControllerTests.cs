using UserService.Common.Exceptions;
using UserService.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using UserService.Dtos.Responses;
using UserService.Common.Services;
using UserService.Controllers;
using LanguageExt.Common;
using AutoFixture;
using AutoMapper;
using Moq;

namespace UserServiceTests.Controllers;

public class UserControllerTests
{
    private readonly UserController _sut;
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
    private readonly Mock<AccountService> _userServiceMock = new Mock<AccountService>();
    private readonly Fixture _fixture;

    public UserControllerTests()
    {
        _sut = new UserController(_userServiceMock.Object, _mapperMock.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task Controller_ShouldReturnOkResponse()
    {
        //Arrange
        var request = _fixture.Create<RegisterRequest>();
        var response = _fixture.Create<UserResponse>();
        
        _userServiceMock.Setup(
                u => u.Register(request))
            .ReturnsAsync(new Result<UserResponse>(response));

        //Act
        var result = await _sut.Register(request);
        
        //Assert
        Assert.Equal(200, ((ObjectResult)result).StatusCode);
    }

    [Fact]
    public async Task Controller_ShouldReturnConflictResponse_WhenEmailAlreadyExists()
    {
        //Arrange
        var request = _fixture.Create<RegisterRequest>();
        var exception = new DuplicateEmailException("Email already exists");
        
        _userServiceMock.Setup(
                u => u.Register(request))
            .ReturnsAsync(new Result<UserResponse>(exception));
        
        //Act
        var result = await _sut.Register(request);

        //Assert
        Assert.Equal(409, ((ObjectResult)result).StatusCode);
    }
}