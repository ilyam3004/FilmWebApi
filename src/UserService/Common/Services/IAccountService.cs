using LanguageExt.Common;
using UserService.Dtos.Requests;
using UserService.Dtos.Responses;

namespace UserService.Common.Services;

public interface IAccountService
{
    Task<Result<UserResponse>> Register(RegisterRequest request);
    Task<Result<UserResponse>> Login(LoginRequest request);
}