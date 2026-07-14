using AIWorkHub.Application.Common.Interfaces;
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
public sealed class ReportsController
    : ControllerBase
{
    private readonly ISender _sender;
    private readonly IReportExportService _exportService;
    public ReportsController(
        ISender sender,
        IReportExportService exportService)
    {
        _sender = sender;
        _exportService = exportService;
    }

    [HttpGet("dashboard-analytics")]
    public async Task<IActionResult> DashboardAnalytics(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetDashboardAnalyticsQuery(),
            cancellationToken);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpGet("project/{projectId:guid}")]
    public async Task<IActionResult> GetProjectReport(
        Guid projectId,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
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
        var result = await _sender.Send(
            new GetUserProductivityQuery(userId),
            cancellationToken);

        if (result.IsFailure)
            return BadRequest(result);

        return Ok(result);
    }

    // Excel endpoint for dashboard analytics

    [HttpGet("dashboard-analytics/export/excel")]
    public async Task<IActionResult> ExportDashboardExcel(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetDashboardAnalyticsQuery(),
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        var bytes = await _exportService.ExportDashboardExcelAsync(
            result.Value,
            cancellationToken);

        return File(
            bytes,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"DashboardAnalytics-{DateTime.UtcNow:yyyyMMddHHmmss}.xlsx");
    }

    // CSV endpoint for dashboard analytics
    
    [HttpGet("dashboard-analytics/export/csv")]
    public async Task<IActionResult> ExportDashboardCsv(
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetDashboardAnalyticsQuery(),
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        var bytes = await _exportService.ExportDashboardCsvAsync(
            result.Value,
            cancellationToken);

        return File(
            bytes,
            "text/csv",
            $"DashboardAnalytics-{DateTime.UtcNow:yyyyMMddHHmmss}.csv");
    }


}
