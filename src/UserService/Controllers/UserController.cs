using UserService.Common.Services;
using UserService.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace UserService.Controllers;

[ApiController]
[Route("users")]
public class UserController : ApiController
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public UserController(
        IUserService userService,
        IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
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
