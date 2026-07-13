using AIWorkHub.Application.Features.Kanban.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Kanban.GetBoard;

public sealed record GetKanbanBoardQuery(
    Guid ProjectId)
    : IRequest<Result<KanbanBoardResponse>>;
