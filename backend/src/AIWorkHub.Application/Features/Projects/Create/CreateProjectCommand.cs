using AIWorkHub.Application.Features.Projects.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.Create;

public sealed record CreateProjectCommand(CreateProjectRequest Request)
    : IRequest<Result<ProjectResponse>>;
