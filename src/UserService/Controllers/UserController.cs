using UserService.Dtos.Requests;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UserService.Common.Services;

namespace UserService.Controllers;

[ApiController]
[Route("users")]
public class UserController : ApiController
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;
    
    public UserController(
        IAccountService accountService,
        IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _accountService.Register(request);

        return result.Match<IActionResult>(Ok, Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _accountService.Login(request);

        return result.Match<IActionResult>(Ok, Problem);
    }
}
