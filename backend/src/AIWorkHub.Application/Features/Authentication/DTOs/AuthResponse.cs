namespace AIWorkHub.Application.Features.Authentication.DTOs;

public sealed class AuthResponse
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public DateTime ExpiresAtUtc { get; set; }
}
