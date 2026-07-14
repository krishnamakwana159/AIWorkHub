using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.AI.ProjectSummary;

public sealed record ProjectSummaryQuery(
    Guid ProjectId)
    : IRequest<Result<string>>;
