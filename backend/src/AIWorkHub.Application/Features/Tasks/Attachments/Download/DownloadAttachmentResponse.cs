namespace AIWorkHub.Application.Features.Tasks.Attachments.Download;

public sealed class DownloadAttachmentResponse
{
    public Stream Stream { get; init; } = Stream.Null;

    public string FileName { get; init; } = string.Empty;

    public string ContentType { get; init; } = "application/octet-stream";
}
