using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Interfaces.Repositories;

public interface ITaskTagRepository : IRepository<TaskTag>
{
    Task<TaskTag?> GetAsync(
        Guid workTaskId,
        Guid tagId,
        CancellationToken cancellationToken);

    Task<List<TaskTag>> GetByTaskIdAsync(
        Guid workTaskId,
        CancellationToken cancellationToken);

    Task<List<TaskTag>> GetByTaskIdsAsync(
        List<Guid> workTaskIds,
        CancellationToken cancellationToken);
}
