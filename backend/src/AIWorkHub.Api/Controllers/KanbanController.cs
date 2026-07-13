using AIWorkHub.Application.Features.Kanban.GetBoard;
using AIWorkHub.Application.Features.Kanban.MoveTask;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/kanban")]
public sealed class KanbanController(ISender sender)
    : ControllerBase
{
    [HttpGet("{projectId:guid}")]
    public async Task<IActionResult> GetBoard(
        Guid projectId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetKanbanBoardQuery(projectId),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPatch("tasks/{taskId:guid}/move")]
    public async Task<IActionResult> MoveTask(
        Guid taskId,
        MoveTaskRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new MoveTaskCommand(taskId, request),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }
}
