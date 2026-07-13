using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AIWorkHub.Persistence.Repositories;

public sealed class ProjectRepository(AppDbContext context)
    : RepositoryBase<Project>(context), IProjectRepository
{
    public async Task<bool> ExistsAsync(
        Guid projectId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(
            x => x.Id == projectId && !x.IsDeleted,
            cancellationToken);
    }

    public async Task<Project?> GetByIdWithOwnerAsync(
        Guid id,
        Guid ownerId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.OwnerId == ownerId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Project>> GetAllAsync(
        Guid ownerId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(x => x.OwnerId == ownerId)
            .OrderByDescending(x => x.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Project>> SearchAsync(
        Guid ownerId,
        string? search,
        ProjectStatus? status,
        ProjectPriority? priority,
        bool? favorite,
        bool? archived,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet.Where(x => x.OwnerId == ownerId);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(x => x.Name.Contains(search));

        if (status.HasValue)
            query = query.Where(x => x.Status == status);

        if (priority.HasValue)
            query = query.Where(x => x.Priority == priority);

        if (favorite.HasValue)
            query = query.Where(x => x.IsFavorite == favorite);

        if (archived.HasValue)
            query = query.Where(x => x.IsArchived == archived);

        return await query
            .OrderByDescending(x => x.CreatedAtUtc)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetProjectWithTasksAsync(
        Guid projectId,
        CancellationToken cancellationToken)
    {
        return await context.Projects
            .Include(x => x.Tasks)
            .FirstOrDefaultAsync(
                x => x.Id == projectId,
                cancellationToken);
    }

    public async Task<List<Project>> GetAllProjectsAsync(
        CancellationToken cancellationToken)
    {
        return await context.Projects
            .Include(x => x.Tasks)
            .ToListAsync(cancellationToken);
    }
}
