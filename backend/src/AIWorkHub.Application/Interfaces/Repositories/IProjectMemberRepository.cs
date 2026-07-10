using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Interfaces.Repositories;

public interface IProjectMemberRepository
    : IRepository<ProjectMember>
{
    Task<bool> ExistsAsync(
        Guid projectId,
        Guid userId,
        CancellationToken cancellationToken);

    Task<List<ProjectMember>> GetProjectMembersAsync(
        Guid projectId,
        CancellationToken cancellationToken);

    Task<ProjectMember?> GetAsync(
        Guid projectId,
        Guid userId,
        CancellationToken cancellationToken);
}
