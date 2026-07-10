namespace AIWorkHub.Application.Features.Search.DTOs;

public sealed class SearchUserDto
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
