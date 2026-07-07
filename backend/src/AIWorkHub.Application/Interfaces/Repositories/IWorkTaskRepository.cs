using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Interfaces.Repositories;

public interface IWorkTaskRepository : IRepository<WorkTask>
{
    Task<bool> WorkTaskExistsAsync(
        Guid projectId,
        string title,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<WorkTask>> GetByProjectAsync(
        Guid projectId,
        CancellationToken cancellationToken = default);
}
