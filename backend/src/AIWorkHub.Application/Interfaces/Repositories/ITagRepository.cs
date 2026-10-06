using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Interfaces.Repositories;

public interface ITagRepository : IRepository<Tag>
{
    Task<Tag?> GetByNameAsync(
        Guid projectId,
        string name,
        CancellationToken cancellationToken);

    Task<List<Tag>> GetByProjectIdAsync(
        Guid projectId,
        CancellationToken cancellationToken);
}
