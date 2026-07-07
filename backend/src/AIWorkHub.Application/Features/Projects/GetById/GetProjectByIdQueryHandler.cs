using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Projects.DTOs;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.GetById;

public sealed class GetProjectByIdQueryHandler(
    IProjectRepository repository,
    ICurrentUserService currentUser)
    : IRequestHandler<GetProjectByIdQuery, Result<ProjectResponse>>
{
    public async Task<Result<ProjectResponse>> Handle(
        GetProjectByIdQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.UserId, out var ownerId))
            return Result<ProjectResponse>.Failure("Unauthorized.");

        var project = await repository.GetByIdWithOwnerAsync(
            request.Id,
            ownerId,
            cancellationToken);

        if (project is null)
            return Result<ProjectResponse>.Failure("Project not found.");

        return Result<ProjectResponse>.Success(new ProjectResponse
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Color = project.Color,
            Priority = project.Priority,
            Status = project.Status,
            Progress = project.Progress,
            IsArchived = project.IsArchived,
            IsFavorite = project.IsFavorite,
            StartDateUtc = project.StartDateUtc,
            TargetCompletionDateUtc = project.TargetCompletionDateUtc,
            CompletedAtUtc = project.CompletedAtUtc,
            CreatedAtUtc = project.CreatedAtUtc
        });
    }
}
