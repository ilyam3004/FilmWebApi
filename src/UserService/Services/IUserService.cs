using UserService.Dtos.Responses;
using UserService.Dtos.Requests;
using LanguageExt.Common;

namespace UserService.Services;

public interface IUserService
{
    Task<Result<RegisterResponse>> Register(RegisterRequest request);
    Task<LoginResponse> Login(LoginRequest request);
}