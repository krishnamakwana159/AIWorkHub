using AIWorkHub.Application.Common.Models;
using AIWorkHub.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AIWorkHub.Infrastructure.Services;

public sealed class LocalFileStorageService(IWebHostEnvironment environment)
    : IFileStorageService
{
    private readonly string _uploadPath =
        Path.Combine(environment.WebRootPath ?? "wwwroot", "uploads", "tasks");

    public async Task<(string StoredFileName, string FilePath)> SaveAsync(
        FileUploadDto file,
        CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_uploadPath);

        var storedFileName =
            $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var fullPath = Path.Combine(_uploadPath, storedFileName);

        await using var stream = File.Create(fullPath);

        await file.Content.CopyToAsync(stream, cancellationToken);

        return (storedFileName, fullPath);
    }

    public Task<Stream> OpenReadAsync(
        string filePath,
        CancellationToken cancellationToken = default)
    {
        Stream stream = File.OpenRead(filePath);

        return Task.FromResult(stream);
    }

    public Task DeleteAsync(
        string filePath,
        CancellationToken cancellationToken = default)
    {
        if (File.Exists(filePath))
            File.Delete(filePath);

        return Task.CompletedTask;
    }
}
