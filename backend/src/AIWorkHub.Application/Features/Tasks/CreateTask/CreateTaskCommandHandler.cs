using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using AutoMapper;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.CreateTask;

public sealed class CreateTaskCommandHandler(
    IWorkTaskRepository taskRepository,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork,
    IActivityService activityService,
    IMapper mapper)
    : IRequestHandler<CreateTaskCommand, Result<TaskResponse>>
{
    public async Task<Result<TaskResponse>> Handle(
        CreateTaskCommand request,
        CancellationToken cancellationToken)
    {
        if (!await projectRepository.ExistsAsync(request.Request.ProjectId, cancellationToken))
            return Result<TaskResponse>.Failure("Project not found.");

        if (await taskRepository.WorkTaskExistsAsync(
                request.Request.ProjectId,
                request.Request.Title,
                cancellationToken))
            return Result<TaskResponse>.Failure("Task already exists.");

        var entity = new WorkTask
        {
            ProjectId = request.Request.ProjectId,
            Title = request.Request.Title,
            Description = request.Request.Description,
            Priority = request.Request.Priority,
            Status = Domain.Enums.TaskStatus.Todo,
            StartDateUtc = request.Request.StartDateUtc,
            DueDateUtc = request.Request.DueDateUtc,
            EstimatedHours = request.Request.EstimatedHours
        };

        await taskRepository.AddAsync(entity, cancellationToken);

        await activityService.LogAsync(
            ActivityEntityType.Task,
            entity.Id,
            ActivityAction.Created,
            $"Task '{entity.Title}' created.",
            cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<TaskResponse>.Success(
            mapper.Map<TaskResponse>(entity));
        // return Result<TaskResponse>.Success(new TaskResponse
        // {
        //     Id = entity.Id,
        //     ProjectId = entity.ProjectId,
        //     Title = entity.Title,
        //     Description = entity.Description,
        //     Priority = entity.Priority,
        //     Status = entity.Status,
        //     EstimatedHours = entity.EstimatedHours,
        //     ActualHours = entity.ActualHours,
        //     StartDateUtc = entity.StartDateUtc,
        //     DueDateUtc = entity.DueDateUtc,
        //     CompletedAtUtc = entity.CompletedAtUtc,
        //     IsFavorite = entity.IsFavorite,
        //     IsPinned = entity.IsPinned,
        //     Order = entity.Order
        // });
    }
}
