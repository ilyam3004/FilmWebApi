namespace UserService.Common.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string login);
}

