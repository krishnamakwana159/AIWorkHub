using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Interfaces.Repositories;

public interface ITaskAttachmentRepository : IRepository<TaskAttachment>
{
    Task<IReadOnlyList<TaskAttachment>> GetByTaskIdAsync(
        Guid taskId,
        CancellationToken cancellationToken = default);

    Task<TaskAttachment?> GetByIdWithUserAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}
