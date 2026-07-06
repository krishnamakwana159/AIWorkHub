using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Domain.Entities;
using AIWorkHub.SharedKernel.Constants;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AIWorkHub.Infrastructure.Authentication;

public sealed class JwtTokenGenerator(
    IOptions<JwtSettings> options)
    : IJwtTokenGenerator
{
    private readonly JwtSettings _settings = options.Value;

    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ApplicationClaimTypes.UserId, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_settings.SecretKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
