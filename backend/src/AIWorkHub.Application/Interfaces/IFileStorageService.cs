using AIWorkHub.Application.Common.Models;

namespace AIWorkHub.Application.Interfaces;

public interface IFileStorageService
{
    Task<(string StoredFileName, string FilePath)> SaveAsync(
        FileUploadDto file,
        CancellationToken cancellationToken);

    Task<Stream> OpenReadAsync(
        string filePath,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        string filePath,
        CancellationToken cancellationToken = default);
}
