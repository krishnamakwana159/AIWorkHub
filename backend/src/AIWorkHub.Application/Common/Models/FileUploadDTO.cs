namespace AIWorkHub.Application.Common.Models;

public sealed class FileUploadDto
{
    public Stream Content { get; init; } = Stream.Null;

    public string FileName { get; init; } = string.Empty;

    public string ContentType { get; init; } = string.Empty;

    public long Length { get; init; }
}
