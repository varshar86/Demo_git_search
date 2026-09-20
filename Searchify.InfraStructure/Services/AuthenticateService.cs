using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Searchify.Domain.Interfaces;
using Searchify.Domain.Model;

namespace Searchify.Infrastructure.Services;

public class AuthenticateService : IAuthenticateService
{
    private readonly IConfiguration _config;

    public AuthenticateService(IConfiguration config)
    {
        _config = config;
    }

    public Task<string> AuthenticateUser(User user)
    {
        if (!string.Equals(user.UserName, "Varun", StringComparison.OrdinalIgnoreCase) ||
            user.Password != "Kumar")
        {
            return Task.FromResult("Username or Password incorrect");
        }

        var issuer = _config["Jwt:Issuer"]
            ?? throw new InvalidOperationException("Jwt:Issuer is missing from configuration.");
        var audience = _config["Jwt:Audience"]
            ?? throw new InvalidOperationException("Jwt:Audience is missing from configuration.");
        var key = _config["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key is missing from configuration.");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var jwtToken = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: credentials);

        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwtToken));
    }
}
