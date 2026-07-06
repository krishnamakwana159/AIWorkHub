using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Authentication.DTOs;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Domain.Entities;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Authentication.Register;

public sealed class RegisterCommandHandler(IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork,
    IJwtTokenGenerator jwtTokenGenerator,
    IRefreshTokenRepository refreshTokenRepository)
    : IRequestHandler<RegisterCommand, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByEmailAsync(request.Request.Email, cancellationToken))
        {
            return Result<AuthResponse>.Failure("Email is already registered.");
        }

        var user = new User
        {
            FirstName = request.Request.FirstName,
            LastName = request.Request.LastName,
            Email = request.Request.Email.Trim().ToLowerInvariant(),
            PasswordHash = passwordHasher.HashPassword(request.Request.Password),
            IsActive = true
        };

        await userRepository.AddAsync(user, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

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
