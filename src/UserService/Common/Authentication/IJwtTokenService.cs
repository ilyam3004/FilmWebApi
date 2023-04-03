using System.Security.Claims;

namespace UserService.Common.Authentication;

public interface IJwtTokenService
{
    string GenerateToken(Guid userId, string login);
    string DecodeJwt(string token);
    bool CanReadToken(string token);
}

