namespace AIWorkHub.Application.Features.Tasks.Comments.DTOs;

public sealed class CommentResponse
{
    public Guid Id { get; set; }

    public Guid TaskId { get; set; }

    public Guid UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Comment { get; set; } = string.Empty;

    public DateTimeOffset CreatedAtUtc { get; set; }
}
