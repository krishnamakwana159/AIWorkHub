using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Common.Interfaces;

public interface IProjectRepository : IRepository<Project>
{
    Task<bool> ExistsAsync(
        Guid projectId,
        CancellationToken cancellationToken = default);

    Task<Project?> GetByIdWithOwnerAsync(
        Guid id,
        Guid ownerId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Project>> GetAllAsync(
        Guid ownerId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Project>> SearchAsync(
        Guid ownerId,
        string? search,
        ProjectStatus? status,
        ProjectPriority? priority,
        bool? favorite,
        bool? archived,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<Project?> GetProjectWithTasksAsync(
        Guid projectId,
        CancellationToken cancellationToken);

    Task<List<Project>> GetAllProjectsAsync(
        CancellationToken cancellationToken);
}
