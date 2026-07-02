using System.Security.Claims;
using AIWorkHub.SharedKernel.Constants;
using AIWorkHub.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AIWorkHub.Infrastructure.Services;

/// <summary>
/// Resolves current-user information from the HTTP context.
/// </summary>
public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    /// <inheritdoc />
    public string? UserId
    {
        get
        {
            var user = httpContextAccessor.HttpContext?.User;

            return user?.FindFirstValue(ApplicationClaimTypes.UserId)
                ?? user?.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? user?.FindFirstValue("sub");
        }
    }

    /// <inheritdoc />
    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true;
}
