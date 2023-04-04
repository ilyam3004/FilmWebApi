using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserService.Common.Services;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace UserService.Common.Authentication;

public class JwtTokenService : IJwtTokenService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;
    
    public JwtTokenService(
        IDateTimeProvider dateTimeProvider, 
        IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }
    
    public string GenerateToken(Guid userId, string login)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, login)
        };

        var securityKey = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityKey);
    }

    public string DecodeJwt(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(token);
        var userId = jwtSecurityToken.Claims.First(x => 
                x.Type == JwtRegisteredClaimNames.UniqueName)
            .Value;
        
        Console.WriteLine(userId);
        return userId;
    }

    public bool CanReadToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        return handler.CanReadToken(token);
    }
}