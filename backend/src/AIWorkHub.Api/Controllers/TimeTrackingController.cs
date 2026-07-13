using AIWorkHub.Application.Features.TimeTracking.AddManualEntry;
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
    [HttpPost("start")]
    public async Task<IActionResult> Start(
        StartTimerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new StartTimerCommand(request),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }


    [HttpPost("stop")]
    public async Task<IActionResult> Stop(
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new StopTimerCommand(),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
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
            return BadRequest(result);

        return Ok(result);
    }

}
