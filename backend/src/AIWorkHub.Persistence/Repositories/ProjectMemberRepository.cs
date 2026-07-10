using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIWorkHub.Persistence.Repositories;

public sealed class ProjectMemberRepository(AppDbContext context)
    : RepositoryBase<ProjectMember>(context),
      IProjectMemberRepository
{
    public async Task<bool> ExistsAsync(
        Guid projectId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await context.ProjectMembers.AnyAsync(
            x => x.ProjectId == projectId &&
                x.UserId == userId,
            cancellationToken);
    }

    public async Task<List<ProjectMember>> GetProjectMembersAsync(
        Guid projectId,
        CancellationToken cancellationToken)
    {
        return await context.ProjectMembers
            .Include(x => x.User)
            .Where(x => x.ProjectId == projectId)
            .OrderBy(x => x.Role)
            .ThenBy(x => x.User.FirstName)
            .ToListAsync(cancellationToken);
    }

    public async Task<ProjectMember?> GetAsync(
        Guid projectId,
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await context.ProjectMembers
            .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == userId, cancellationToken);
    }
}
