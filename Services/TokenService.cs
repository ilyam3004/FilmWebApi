using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FilmWebApi.Services
{
    public class TokenService : ITokenService
    {
        private const string KEY = "mysupersecret_secretkey!123";
        private const int LIFETIME = 365; 
        
        public string CreateToken(User user)
        {
            var identity = GetIdentity(user);
            var jwt = new JwtSecurityToken(
                notBefore: DateTime.UtcNow,
                claims: identity.Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(LIFETIME)),
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        private ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, user.Login)
            };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, 
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
            => new (Encoding.ASCII.GetBytes(KEY));
    }
}