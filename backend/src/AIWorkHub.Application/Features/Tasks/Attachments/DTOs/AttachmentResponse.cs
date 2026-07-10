namespace AIWorkHub.Application.Features.Tasks.Attachments.DTOs;

public sealed class AttachmentResponse
{
    public Guid Id { get; set; }

    public Guid TaskId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public long FileSize { get; set; }

    public string UploadedBy { get; set; } = string.Empty;

    public DateTimeOffset UploadedAtUtc { get; set; }
}
