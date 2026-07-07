using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Projects.DTOs;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Domain.Entities;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.Create;

public sealed class CreateProjectCommandHandler(
    IProjectRepository projectRepository,
    ICurrentUserService currentUserService,
    IActivityService activityService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProjectCommand, Result<ProjectResponse>>
{
    public async Task<Result<ProjectResponse>> Handle(
        CreateProjectCommand request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUserService.UserId, out var ownerId))
        {
            return Result<ProjectResponse>.Failure("Unauthorized.");
        }

        if (await projectRepository.ExistsAsync(
                ownerId,
                cancellationToken))
        {
            return Result<ProjectResponse>.Failure(
                "Project already exists.");
        }

        var project = new Project
        {
            Name = request.Request.Name,
            Description = request.Request.Description,
            Color = request.Request.Color,

            Priority = request.Request.Priority,

            Status = Domain.Enums.ProjectStatus.NotStarted,

            Progress = 0,

            StartDateUtc = request.Request.StartDateUtc,

            TargetCompletionDateUtc = request.Request.TargetCompletionDateUtc,

            OwnerId = ownerId,

            IsArchived = false,

            IsFavorite = false
        };

        await projectRepository.AddAsync(project, cancellationToken);

        await activityService.LogAsync(
            Domain.Enums.ActivityEntityType.Project,
            project.Id,
            Domain.Enums.ActivityAction.Created,
            $"Project '{project.Name}' created.",
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ProjectResponse>.Success(
            new ProjectResponse
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Color = project.Color,
                IsArchived = project.IsArchived,
                Priority = project.Priority,
                Status = project.Status,
                Progress = project.Progress,
                StartDateUtc = project.StartDateUtc,
                TargetCompletionDateUtc = project.TargetCompletionDateUtc,
                CompletedAtUtc = project.CompletedAtUtc,
                CreatedAtUtc = project.CreatedAtUtc
            });
    }
}
