namespace AIWorkHub.SharedKernel.Constants;

/// <summary>
/// Defines application authorization policy names.
/// </summary>
public static class ApplicationPolicies
{
    /// <summary>
    /// Policy requiring administrator access.
    /// </summary>
    public const string RequireAdministrator = "RequireAdministrator";

    /// <summary>
    /// Policy requiring an authenticated user.
    /// </summary>
    public const string RequireAuthenticatedUser = "RequireAuthenticatedUser";
}
