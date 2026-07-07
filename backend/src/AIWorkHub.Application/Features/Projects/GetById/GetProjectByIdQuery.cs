using AIWorkHub.Application.Features.Projects.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.GetById;

public sealed record GetProjectByIdQuery(Guid Id)
    : IRequest<Result<ProjectResponse>>;
