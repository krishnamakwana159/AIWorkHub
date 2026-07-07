using AIWorkHub.Application.Features.Tasks.Assign;
using AIWorkHub.Application.Features.Tasks.CreateTask;
using AIWorkHub.Application.Features.Tasks.DeleteTask;
using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.Application.Features.Tasks.GetAllTasks;
using AIWorkHub.Application.Features.Tasks.GetTaskById;
using AIWorkHub.Application.Features.Tasks.UpdateStatus;
using AIWorkHub.Application.Features.Tasks.UpdateTask;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIWorkHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public sealed class TasksController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new CreateTaskCommand(request),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] Guid projectId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetTasksQuery(projectId),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetTaskByIdQuery(id),
            cancellationToken);

        if (result.IsFailure)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new UpdateTaskCommand(id, request),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new DeleteTaskCommand(id),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/assign")]
    public async Task<IActionResult> Assign(
        Guid id,
        AssignTaskRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new AssignTaskCommand(id, request.UserId),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid id,
        UpdateTaskStatusRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new UpdateTaskStatusCommand(id, request.Status),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

}
