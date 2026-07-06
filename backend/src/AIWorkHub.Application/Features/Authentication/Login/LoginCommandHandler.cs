using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Authentication.DTOs;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Authentication.Login;

public sealed class LoginCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(
            request.Request.Email,
            cancellationToken);

        if (user is null)
        {
            return Result<AuthResponse>.Failure("Invalid email or password.");
        }

        if (!user.IsActive)
        {
            return Result<AuthResponse>.Failure("User account is inactive.");
        }

        var validPassword = passwordHasher.VerifyPassword(
            request.Request.Password,
            user.PasswordHash);

        if (!validPassword)
        {
            return Result<AuthResponse>.Failure("Invalid email or password.");
        }

        var accessToken = jwtTokenGenerator.GenerateAccessToken(user);

        var refreshTokenValue = jwtTokenGenerator.GenerateRefreshToken();

        var refreshToken = new Domain.Entities.RefreshToken
        {
            Token = refreshTokenValue,
            UserId = user.Id,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(7)
        };

        await refreshTokenRepository.AddAsync(
            refreshToken,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<AuthResponse>.Success(new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue,
            ExpiresAtUtc = DateTime.UtcNow.AddMinutes(30)
        });
    }
}
