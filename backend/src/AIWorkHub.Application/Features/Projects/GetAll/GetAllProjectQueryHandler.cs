using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Projects.DTOs;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.GetAll;

public sealed class GetAllProjectsQueryHandler(
    IProjectRepository repository,
    ICurrentUserService currentUser)
    : IRequestHandler<GetAllProjectsQuery, Result<List<ProjectResponse>>>
{
    public async Task<Result<List<ProjectResponse>>> Handle(
        GetAllProjectsQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.UserId, out var ownerId))
            return Result<List<ProjectResponse>>.Failure("Unauthorized.");

        var projects = await repository.GetAllAsync(ownerId, cancellationToken);

        return Result<List<ProjectResponse>>.Success(
            projects.Select(x => new ProjectResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Color = x.Color,
                Priority = x.Priority,
                Status = x.Status,
                Progress = x.Progress,
                IsArchived = x.IsArchived,
                IsFavorite = x.IsFavorite,
                StartDateUtc = x.StartDateUtc,
                TargetCompletionDateUtc = x.TargetCompletionDateUtc,
                CompletedAtUtc = x.CompletedAtUtc,
                CreatedAtUtc = x.CreatedAtUtc
            }).ToList());
    }
}
