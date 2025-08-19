using Ecommerce.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Application.Services
{
    public class AuthService(IConfiguration config)
    {
        public string? Login(string username, string password)
        {
            if (username != "admin" || password != "#Adm1234")
                return null;

            var jwt = new Secrets.Jwt(config);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: [
                    new Claim("Username", username),
                ],
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                expires: DateTime.UtcNow.AddMinutes(jwt.ExpiresMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
