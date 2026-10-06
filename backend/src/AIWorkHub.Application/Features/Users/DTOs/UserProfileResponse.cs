namespace AIWorkHub.Application.Features.Users.DTOs;

public sealed class UserProfileResponse
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public DateTimeOffset CreatedAtUtc { get; set; }
}
