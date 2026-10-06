using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Interfaces.Repositories;

public interface IWorkTaskRepository : IRepository<WorkTask>
{
    Task<WorkTask?> GetByIdWithAssigneeAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<List<WorkTask>> GetAllTasksWithProjectAsync(
        CancellationToken cancellationToken);

    Task<List<WorkTask>> GetByProjectIdsWithDueDateAsync(
        List<Guid> projectIds,
        CancellationToken cancellationToken = default);

    Task<List<WorkTask>> GetByProjectIdsAsync(
        List<Guid> projectIds,
        CancellationToken cancellationToken = default);

    Task<bool> WorkTaskExistsAsync(
        Guid projectId,
        string title,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<WorkTask>> GetByProjectAsync(
        Guid projectId,
        CancellationToken cancellationToken = default);

    Task<List<WorkTask>> GetKanbanTasksAsync(
        Guid projectId,
        CancellationToken cancellationToken);

    Task<List<WorkTask>> GetTasksByStatusAsync(
        Guid projectId,
        WorkTaskStatus status,
        CancellationToken cancellationToken);

    Task UpdateRangeAsync(
        IEnumerable<WorkTask> tasks,
        CancellationToken cancellationToken);

    Task<List<WorkTask>> GetOrderedTasksAsync(
        Guid projectId,
        WorkTaskStatus status,
        CancellationToken cancellationToken);

    Task<List<WorkTask>> GetAllTasksAsync(
        CancellationToken cancellationToken);

    Task ToggleFavoriteAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task TogglePinAsync(
        Guid id,
        CancellationToken cancellationToken);
}
