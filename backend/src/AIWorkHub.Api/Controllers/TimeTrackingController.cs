using AIWorkHub.Application.Features.TimeTracking.AddManualEntry;
using AIWorkHub.Application.Features.TimeTracking.GetRunningTimer;
using AIWorkHub.Application.Features.TimeTracking.GetTaskEntries;
using AIWorkHub.Application.Features.TimeTracking.StartTimer;
using AIWorkHub.Application.Features.TimeTracking.StopTimer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIWorkHub.Api.Controllers;

[Authorize]
[Route("api/time")]
[ApiController]
public sealed class TimeTrackingController(ISender sender)
    : ControllerBase
{
    [HttpGet("running")]
    public async Task<IActionResult> GetRunning(
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetRunningTimerQuery(),
            cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors);
    }

    [HttpGet("task/{taskId:guid}")]
    public async Task<IActionResult> GetTaskEntries(
        Guid taskId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetTaskTimeEntriesQuery(taskId),
            cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors);
    }

    [HttpPost("start")]
    public async Task<IActionResult> Start(
        StartTimerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new StartTimerCommand(request),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Errors);

        return NoContent();
    }


    [HttpPost("stop")]
    public async Task<IActionResult> Stop(
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new StopTimerCommand(),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Errors);

        return NoContent();
    }

    [HttpPost("manual")]
    public async Task<IActionResult> AddManualEntry(
        AddManualEntryRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new AddManualEntryCommand(request),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Errors);

        return NoContent();
    }

}
