using UserService.Dtos.Responses;
using UserService.Dtos.Requests;

namespace UserService.Services;

public interface IUserService
{
    Task<RegisterResponse> Register(RegisterRequest request);
    Task<LoginResponse> Login(LoginRequest request);
}