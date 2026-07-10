using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Projects.DTOs;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.Create;

public sealed class CreateProjectCommandHandler(
    IProjectRepository projectRepository,
    ICurrentUserService currentUserService,
    IActivityService activityService,
    INotificationService notificationService,
    IProjectMemberRepository projectMemberRepository,
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

            Status = ProjectStatus.NotStarted,

            Progress = 0,

            StartDateUtc = request.Request.StartDateUtc,

            TargetCompletionDateUtc = request.Request.TargetCompletionDateUtc,

            OwnerId = ownerId,

            IsArchived = false,

            IsFavorite = false
        };

        await projectRepository.AddAsync(project, cancellationToken);

        var ownerMember = new ProjectMember
        {
            Project = project,
            UserId = project.OwnerId,
            Role = ProjectRole.Owner
        };

        await projectMemberRepository.AddAsync(
            ownerMember,
            cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await notificationService.NotifyAsync(
            ownerId,
            "Project Created",
            $"Project '{project.Name}' has been created.",
            NotificationType.ProjectCreated,
            $"/projects/{project.Id}",
            cancellationToken);

        await activityService.LogAsync(
            ActivityEntityType.Project,
            project.Id,
            ActivityAction.Created,
            $"Project '{project.Name}' created.",
            cancellationToken);

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
