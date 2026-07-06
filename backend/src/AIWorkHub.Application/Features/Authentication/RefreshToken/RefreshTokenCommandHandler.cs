using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Authentication.DTOs;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Domain.Entities;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Authentication.RefreshToken;

public sealed class RefreshTokenCommandHandler(
    IRefreshTokenRepository refreshTokenRepository,
    IAuthenticationService authenticationService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RefreshTokenCommand, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var refreshToken = await refreshTokenRepository.GetByTokenAsync(
            request.Request.RefreshToken,
            cancellationToken);

        if (refreshToken is null)
        {
            return Result<AuthResponse>.Failure("Invalid refresh token.");
        }

        if (refreshToken.IsRevoked)
        {
            return Result<AuthResponse>.Failure("Refresh token has been revoked.");
        }

        if (refreshToken.ExpiresAtUtc <= DateTime.UtcNow)
        {
            return Result<AuthResponse>.Failure("Refresh token has expired.");
        }

        refreshToken.IsRevoked = true;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var authResponse = await authenticationService.CreateAuthResponseAsync(
            refreshToken.User,
            cancellationToken);

        return Result<AuthResponse>.Success(authResponse);
    }
}
