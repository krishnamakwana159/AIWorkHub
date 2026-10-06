using AIWorkHub.Application.Features.Tags.DTOs;
using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.GetAllTasks;

public sealed class GetTasksQueryHandler(
    IWorkTaskRepository repository,
    ITaskTagRepository taskTagRepository)
    : IRequestHandler<GetTasksQuery, Result<IReadOnlyList<TaskResponse>>>
{
    public async Task<Result<IReadOnlyList<TaskResponse>>> Handle(
        GetTasksQuery request,
        CancellationToken cancellationToken)
    {
        var tasks = await repository.GetByProjectAsync(
            request.ProjectId,
            cancellationToken);

        var taskIds = tasks.Select(x => x.Id).ToList();

        var taskTags = await taskTagRepository.GetByTaskIdsAsync(
            taskIds,
            cancellationToken);

        var tagsByTask = taskTags
            .GroupBy(x => x.WorkTaskId)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => new TagResponse
                {
                    Id = x.Tag.Id,
                    Name = x.Tag.Name,
                    Color = x.Tag.Color
                }).ToList());

        var response = tasks
            .Select(x => new TaskResponse
            {
                Id = x.Id,
                ProjectId = x.ProjectId,
                Title = x.Title,
                Description = x.Description,
                Priority = x.Priority,
                Status = x.Status,
                EstimatedHours = x.EstimatedHours,
                ActualHours = x.ActualHours,
                StartDateUtc = x.StartDateUtc,
                DueDateUtc = x.DueDateUtc,
                CompletedAtUtc = x.CompletedAtUtc,
                IsPinned = x.IsPinned,
                IsFavorite = x.IsFavorite,
                Order = x.Order,
                Tags = tagsByTask.TryGetValue(x.Id, out var tags)
                    ? tags
                    : new List<TagResponse>()
            })
            .ToList();

        return Result<IReadOnlyList<TaskResponse>>.Success(response);
    }
}
