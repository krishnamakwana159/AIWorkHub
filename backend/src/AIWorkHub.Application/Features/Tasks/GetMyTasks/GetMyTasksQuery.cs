using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tasks.GetMyTasks;

public sealed record GetMyTasksQuery
    : IRequest<Result<IReadOnlyList<MyTaskResponse>>>;
