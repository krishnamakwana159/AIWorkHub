using AIWorkHub.Application.Features.Reports.DashboardAnalytics;
using AIWorkHub.Application.Features.Reports.ProjectReport;
using AIWorkHub.Application.Features.Reports.UserProductivity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIWorkHub.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/reports")]
public sealed class ReportsController(ISender sender)
    : ControllerBase
{
     [HttpGet("dashboard-analytics")]
    public async Task<IActionResult> DashboardAnalytics(
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetDashboardAnalyticsQuery(),
            cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("project/{projectId:guid}")]
    public async Task<IActionResult> GetProjectReport(
        Guid projectId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetProjectReportQuery(projectId),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetUserProductivity(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetUserProductivityQuery(userId),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }
}
