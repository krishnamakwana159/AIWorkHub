using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIWorkHub.Persistence.Repositories;

public sealed class TagRepository(AppDbContext context)
    : RepositoryBase<Tag>(context), ITagRepository
{
    public async Task<Tag?> GetByNameAsync(
        Guid projectId,
        string name,
        CancellationToken cancellationToken)
    {
        return await context.Tags.FirstOrDefaultAsync(
            x => x.ProjectId == projectId && x.Name == name,
            cancellationToken);
    }

    public async Task<List<Tag>> GetByProjectIdAsync(
        Guid projectId,
        CancellationToken cancellationToken)
    {
        return await context.Tags
            .Where(x => x.ProjectId == projectId)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }
}
