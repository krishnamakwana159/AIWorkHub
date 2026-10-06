using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.GetAllTasksAcrossProjects;

public sealed record GetAllTasksAcrossProjectsQuery
    : IRequest<Result<IReadOnlyList<AdminTaskResponse>>>;
