using AIWorkHub.Application.Features.Dashboard.GetDashboard;
using AIWorkHub.Application.Features.Reports.DashboardAnalytics;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AIWorkHub.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public sealed class DashboardController(ISender sender)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetDashboardQuery(),
            cancellationToken);

        return Ok(result);
    }

     [HttpGet("dashboard-analytics")]
    public async Task<IActionResult> DashboardAnalytics(
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetDashboardAnalyticsQuery(),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }
}
