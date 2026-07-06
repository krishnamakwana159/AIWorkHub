using AIWorkHub.Application.Features.Authentication.DTOs;
using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Common.Interfaces;

public interface IAuthenticationService
{
    Task<AuthResponse> CreateAuthResponseAsync(
        User user,
        CancellationToken cancellationToken = default);
}
