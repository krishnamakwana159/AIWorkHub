using AIWorkHub.SharedKernel.Entities;

namespace AIWorkHub.Domain.Entities;

public sealed class TaskAttachment : AuditableEntity
{
    public Guid TaskId { get; set; }

    public WorkTask Task { get; set; } = null!;

    public string FileName { get; set; } = string.Empty;

    public string StoredFileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public long FileSize { get; set; }

    public string FilePath { get; set; } = string.Empty;

    public Guid UploadedBy { get; set; }

    public User UploadedByUser { get; set; } = null!;
}
