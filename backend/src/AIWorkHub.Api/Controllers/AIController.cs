using AIWorkHub.Application.Features.AI.DTOs;
using AIWorkHub.Application.Features.AI.GenerateBreakdown;
using AIWorkHub.Application.Features.AI.GenerateTaskDescription;
using AIWorkHub.Application.Features.AI.ProjectSummary;
using AIWorkHub.Application.Features.AI.SuggestPriority;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;

namespace AIWorkHub.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/ai")]
public sealed class AIController(
    ISender sender)
    : ControllerBase
{
    [HttpPost("task-description")]
    public async Task<IActionResult> GenerateTaskDescription(
        GenerateTaskDescriptionRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GenerateTaskDescriptionCommand(request),
            cancellationToken);

        return result.IsSuccess
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPost("generate-description")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateDescription(
        GenerateTaskDescriptionRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GenerateTaskDescriptionCommand(new GenerateTaskDescriptionRequest()
            {
                Title = request.Title
            }),
            cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors);
    }

    [HttpPost("generate-breakdown")]
    [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GenerateBreakdown(
        GenerateTaskBreakdownRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GenerateBreakdownCommand(
                request.Title,
                request.Description),
            cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors);
    }

    [HttpPost("suggest-priority")]
    public async Task<IActionResult> SuggestPriority(
     SuggestPriorityRequest request,
     CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new SuggestPriorityCommand(
                request.Title,
                request.Description),
            cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Errors);
    }

    [HttpGet("project-summary/{projectId:guid}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> ProjectSummary(
        Guid projectId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new ProjectSummaryQuery(projectId),
            cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(result.Errors);
    }
}
