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

        var filtered = projects.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim();
            filtered = filtered.Where(x =>
                x.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        if (request.Status.HasValue)
            filtered = filtered.Where(x => x.Status == request.Status.Value);

        if (request.Priority.HasValue)
            filtered = filtered.Where(x => x.Priority == request.Priority.Value);

        if (request.Favorite.HasValue)
            filtered = filtered.Where(x => x.IsFavorite == request.Favorite.Value);

        if (request.Archived.HasValue)
            filtered = filtered.Where(x => x.IsArchived == request.Archived.Value);

        var page = request.Page < 1 ? 1 : request.Page;
        var pageSize = request.PageSize is < 1 or > 100 ? 10 : request.PageSize;

        var paged = filtered
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        return Result<List<ProjectResponse>>.Success(
            paged.Select(x => new ProjectResponse
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
