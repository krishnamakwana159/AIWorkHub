using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Authentication.DTOs;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Domain.Entities;
using AIWorkHub.SharedKernel.Interfaces;

namespace AIWorkHub.Infrastructure.Authentication;

public sealed class AuthenticationService(
    IJwtTokenGenerator jwtTokenGenerator,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork)
    : IAuthenticationService
{
    public async Task<AuthResponse> CreateAuthResponseAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        var accessToken = jwtTokenGenerator.GenerateAccessToken(user);

        var refreshTokenValue = jwtTokenGenerator.GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            Token = refreshTokenValue,
            UserId = user.Id,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        await refreshTokenRepository.AddAsync(
            refreshToken,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue,
            ExpiresAtUtc = DateTime.UtcNow.AddMinutes(30)
        };
    }
}
