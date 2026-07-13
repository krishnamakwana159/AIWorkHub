using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Interfaces.Repositories;

public interface IWorkTimeEntryRepository
    : IRepository<WorkTimeEntry>
{
    Task<WorkTimeEntry?> GetRunningTimerAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<List<WorkTimeEntry>> GetTaskEntriesAsync(
        Guid workTaskId,
        CancellationToken cancellationToken);

    Task<List<WorkTimeEntry>> GetUserEntriesAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<List<WorkTimeEntry>> GetAllAsync(
        CancellationToken cancellationToken);

}
