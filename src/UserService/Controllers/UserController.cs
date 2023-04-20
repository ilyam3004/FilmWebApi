using UserService.Common.Services;
using UserService.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UserService.Common.Authentication;

namespace UserService.Controllers;

[ApiController]
[Route("users")]
public class UserController : ApiController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IJwtTokenService _jwtTokenService;

    public UserController(
        IUserService userService,
        IMapper mapper, 
        IJwtTokenService jwtTokenService)
    {
        _userService = userService;
        _mapper = mapper;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        Console.WriteLine("Received request from the api gateway");
        var result = await _userService.Register(request);

        return result.Match<IActionResult>(Ok, Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _userService.Login(request);

        return result.Match<IActionResult>(Ok, Problem);
    }
}
