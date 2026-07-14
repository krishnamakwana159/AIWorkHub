using System.Globalization;
using System.Text;
using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Reports.DashboardAnalytics;
using ClosedXML.Excel;

namespace AIWorkHub.Infrastructure.Services;

public sealed partial class ReportExportService : IReportExportService
{
    public async Task<byte[]> ExportDashboardExcelAsync(
        DashboardAnalyticsResponse report,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        using var workbook = new XLWorkbook();

        BuildOverviewWorksheet(workbook, report);
        BuildMonthlyTrendWorksheet(workbook, report);
        BuildProjectProgressWorksheet(workbook, report);
        BuildTeamPerformanceWorksheet(workbook, report);
        BuildTopPerformersWorksheet(workbook, report);

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return stream.ToArray();
    }

    public async Task<byte[]> ExportDashboardCsvAsync(
        DashboardAnalyticsResponse report,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var builder = new StringBuilder();

        builder.AppendLine("Metric,Value");

        builder.AppendLine($"Generated At,{report.GeneratedAtUtc:u}");
        builder.AppendLine($"Total Projects,{report.Overview.TotalProjects}");
        builder.AppendLine($"Total Users,{report.Overview.TotalUsers}");
        builder.AppendLine($"Total Tasks,{report.Overview.TotalTasks}");
        builder.AppendLine($"Completed Tasks,{report.Overview.CompletedTasks}");
        builder.AppendLine($"Active Tasks,{report.Overview.ActiveTasks}");
        builder.AppendLine($"Overdue Tasks,{report.Overview.OverdueTasks}");
        builder.AppendLine($"Estimated Hours,{report.Overview.EstimatedHours.ToString(CultureInfo.InvariantCulture)}");
        builder.AppendLine($"Actual Hours,{report.Overview.ActualHours.ToString(CultureInfo.InvariantCulture)}");
        builder.AppendLine($"Completion Percentage,{report.Overview.CompletionPercentage.ToString(CultureInfo.InvariantCulture)}");

        return Encoding.UTF8.GetBytes(builder.ToString());
    }

    private static void BuildOverviewWorksheet(
        XLWorkbook workbook,
        DashboardAnalyticsResponse report)
    {
        var sheet = workbook.Worksheets.Add("Overview");

        sheet.Cell(1, 1).Value = "Dashboard Overview";
        sheet.Cell(1, 1).Style.Font.Bold = true;
        sheet.Cell(1, 1).Style.Font.FontSize = 18;

        sheet.Cell(3, 1).Value = "Generated";
        sheet.Cell(3, 2).Value = report.GeneratedAtUtc;

        sheet.Cell(5, 1).Value = "Metric";
        sheet.Cell(5, 2).Value = "Value";

        sheet.Range("A5:B5").Style.Font.Bold = true;

        var row = 6;

        sheet.Cell(row, 1).Value = "Total Projects";
        sheet.Cell(row++, 2).Value = report.Overview.TotalProjects;

        sheet.Cell(row, 1).Value = "Total Users";
        sheet.Cell(row++, 2).Value = report.Overview.TotalUsers;

        sheet.Cell(row, 1).Value = "Total Tasks";
        sheet.Cell(row++, 2).Value = report.Overview.TotalTasks;

        sheet.Cell(row, 1).Value = "Completed Tasks";
        sheet.Cell(row++, 2).Value = report.Overview.CompletedTasks;

        sheet.Cell(row, 1).Value = "Active Tasks";
        sheet.Cell(row++, 2).Value = report.Overview.ActiveTasks;

        sheet.Cell(row, 1).Value = "Overdue Tasks";
        sheet.Cell(row++, 2).Value = report.Overview.OverdueTasks;

        sheet.Cell(row, 1).Value = "Estimated Hours";
        sheet.Cell(row++, 2).Value = report.Overview.EstimatedHours;

        sheet.Cell(row, 1).Value = "Actual Hours";
        sheet.Cell(row++, 2).Value = report.Overview.ActualHours;

        sheet.Cell(row, 1).Value = "Completion %";
        sheet.Cell(row, 2).Value = report.Overview.CompletionPercentage;

        sheet.Columns().AdjustToContents();
    }

    private static void BuildMonthlyTrendWorksheet(
        XLWorkbook workbook,
        DashboardAnalyticsResponse report)
    {
        var sheet = workbook.Worksheets.Add("Monthly Trend");

        sheet.Cell(1, 1).Value = "Year";
        sheet.Cell(1, 2).Value = "Month";
        sheet.Cell(1, 3).Value = "Tasks Created";
        sheet.Cell(1, 4).Value = "Tasks Completed";
        sheet.Cell(1, 5).Value = "Hours Logged";

        sheet.Range("A1:E1").Style.Font.Bold = true;

        var row = 2;

        foreach (var item in report.MonthlyTrend)
        {
            sheet.Cell(row, 1).Value = item.Year;
            sheet.Cell(row, 2).Value = item.Month;
            sheet.Cell(row, 3).Value = item.TasksCreated;
            sheet.Cell(row, 4).Value = item.TasksCompleted;
            sheet.Cell(row, 5).Value = item.HoursLogged;

            row++;
        }

        sheet.Columns().AdjustToContents();
    }

    private static void BuildProjectProgressWorksheet(
        XLWorkbook workbook,
        DashboardAnalyticsResponse report)
    {
        var sheet = workbook.Worksheets.Add("Project Progress");

        sheet.Cell(1, 1).Value = "Project";
        sheet.Cell(1, 2).Value = "Progress (%)";
        sheet.Cell(1, 3).Value = "Total Tasks";
        sheet.Cell(1, 4).Value = "Completed Tasks";
        sheet.Cell(1, 5).Value = "Estimated Hours";
        sheet.Cell(1, 6).Value = "Actual Hours";

        sheet.Range("A1:F1").Style.Font.Bold = true;

        var row = 2;

        foreach (var item in report.ProjectProgress)
        {
            sheet.Cell(row, 1).Value = item.ProjectName;
            sheet.Cell(row, 2).Value = item.Progress;
            sheet.Cell(row, 3).Value = item.TotalTasks;
            sheet.Cell(row, 4).Value = item.CompletedTasks;
            sheet.Cell(row, 5).Value = item.EstimatedHours;
            sheet.Cell(row, 6).Value = item.ActualHours;

            row++;
        }

        sheet.Columns().AdjustToContents();
    }

    private static void BuildTeamPerformanceWorksheet(
        XLWorkbook workbook,
        DashboardAnalyticsResponse report)
    {
        var sheet = workbook.Worksheets.Add("Team Performance");

        sheet.Cell(1, 1).Value = "User";
        sheet.Cell(1, 2).Value = "Assigned Tasks";
        sheet.Cell(1, 3).Value = "Completed Tasks";
        sheet.Cell(1, 4).Value = "Completion Rate";
        sheet.Cell(1, 5).Value = "Hours Logged";

        sheet.Range("A1:E1").Style.Font.Bold = true;

        var row = 2;

        foreach (var item in report.TeamPerformance)
        {
            sheet.Cell(row, 1).Value = item.UserName;
            sheet.Cell(row, 2).Value = item.AssignedTasks;
            sheet.Cell(row, 3).Value = item.CompletedTasks;
            sheet.Cell(row, 4).Value = item.CompletionRate;
            sheet.Cell(row, 5).Value = item.HoursLogged;

            row++;
        }

        sheet.Columns().AdjustToContents();
    }

    private static void BuildTopPerformersWorksheet(
        XLWorkbook workbook,
        DashboardAnalyticsResponse report)
    {
        var sheet = workbook.Worksheets.Add("Top Performers");

        sheet.Cell(1, 1).Value = "Rank";
        sheet.Cell(1, 2).Value = "User";
        sheet.Cell(1, 3).Value = "Productivity Score";

        sheet.Range("A1:C1").Style.Font.Bold = true;

        var row = 2;
        var rank = 1;

        foreach (var item in report.TopPerformers
                    .OrderByDescending(x => x.ProductivityScore))
        {
            sheet.Cell(row, 1).Value = rank++;
            sheet.Cell(row, 2).Value = item.UserName;
            sheet.Cell(row, 3).Value = item.ProductivityScore;

            row++;
        }

        sheet.Columns().AdjustToContents();
    }

}
