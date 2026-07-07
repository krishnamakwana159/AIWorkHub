using AIWorkHub.Application.Features.Projects.DTOs;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.GetAll;

public sealed record GetAllProjectsQuery(
    string? Search,
    ProjectStatus? Status,
    ProjectPriority? Priority,
    bool? Favorite,
    bool? Archived,
    int Page = 1,
    int PageSize = 10)
    : IRequest<Result<List<ProjectResponse>>>;
