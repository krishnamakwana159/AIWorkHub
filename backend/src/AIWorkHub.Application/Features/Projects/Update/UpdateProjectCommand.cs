using AIWorkHub.Application.Features.Projects.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.Update;

public sealed record UpdateProjectCommand(
    Guid Id,
    UpdateProjectRequest Request)
    : IRequest<Result>;
