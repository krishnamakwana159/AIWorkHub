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
    IAuthenticationService authenticationService)
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

        var authResponse = await authenticationService.CreateAuthResponseAsync(
            user,
            cancellationToken);

        return Result<AuthResponse>.Success(authResponse);
    }
}
