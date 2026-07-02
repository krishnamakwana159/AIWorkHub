namespace AIWorkHub.SharedKernel.Interfaces;

/// <summary>
/// Provides information about the current request user.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the current user identifier, when available.
    /// </summary>
    string? UserId { get; }

    /// <summary>
    /// Gets a value indicating whether the current request is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }
}
