namespace AIWorkHub.Application.Features.Search.DTOs;

public sealed class SearchTaskDto
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}
