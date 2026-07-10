namespace AIWorkHub.Application.Features.Search.DTOs;

public sealed class SearchCommentDto
{
    public Guid Id { get; set; }

    public Guid TaskId { get; set; }

    public string Comment { get; set; } = string.Empty;
}
