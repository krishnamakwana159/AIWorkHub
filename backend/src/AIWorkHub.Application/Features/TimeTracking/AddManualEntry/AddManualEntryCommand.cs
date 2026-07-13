using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.TimeTracking.AddManualEntry;

public sealed record AddManualEntryCommand(
    AddManualEntryRequest Request)
    : IRequest<Result>;
