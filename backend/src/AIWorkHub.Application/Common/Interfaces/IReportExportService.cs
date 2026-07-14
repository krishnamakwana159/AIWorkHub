using AIWorkHub.Application.Features.Reports.DashboardAnalytics;
using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Common.Interfaces;

public interface IReportExportService
{
    Task<byte[]> ExportDashboardExcelAsync(
        DashboardAnalyticsResponse dashboard,
        CancellationToken cancellationToken);

    Task<byte[]> ExportDashboardCsvAsync(
        DashboardAnalyticsResponse dashboard,
        CancellationToken cancellationToken);

    // Task<byte[]> ExportProjectExcelAsync(
    //     Project project,
    //     CancellationToken cancellationToken);
}
