using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.Update;

public sealed class UpdateProjectCommandHandler(
    IProjectRepository repository,
    ICurrentUserService currentUser,
    INotificationService notificationService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateProjectCommand, Result>
{
    public async Task<Result> Handle(
        UpdateProjectCommand request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.UserId, out var ownerId))
            return Result.Failure("Unauthorized.");

        var project = await repository.GetByIdWithOwnerAsync(
            request.Id,
            ownerId,
            cancellationToken);

        if (project is null)
            return Result.Failure("Project not found.");

        project.Name = request.Request.Name;
        project.Description = request.Request.Description;
        project.Color = request.Request.Color;
        project.Priority = request.Request.Priority;
        project.Status = request.Request.Status;
        project.StartDateUtc = request.Request.StartDateUtc;
        project.TargetCompletionDateUtc = request.Request.TargetCompletionDateUtc;
        project.IsFavorite = request.Request.IsFavorite;

        repository.Update(project);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await notificationService.NotifyAsync(
            ownerId,
            "Project Updated",
            $"Project '{project.Name}' has been updated.",
            NotificationType.ProjectUpdated,
            $"/projects/{project.Id}",
            cancellationToken);

        return Result.Success();
    }
}
