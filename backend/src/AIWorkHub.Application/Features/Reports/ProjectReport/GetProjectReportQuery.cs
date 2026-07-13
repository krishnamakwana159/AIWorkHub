using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Reports.ProjectReport;

public sealed record GetProjectReportQuery(
    Guid ProjectId)
    : IRequest<Result<ProjectReportResponse>>;
