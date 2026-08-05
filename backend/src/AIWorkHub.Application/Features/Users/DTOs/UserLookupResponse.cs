namespace AIWorkHub.Application.Features.Users.DTOs;

public sealed class UserLookupResponse
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
