namespace AIWorkHub.Application.Features.AI.DTOs;

public sealed class SuggestPriorityRequest
{
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }
}
