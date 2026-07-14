using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.AI.GenerateBreakdown;

public sealed record GenerateBreakdownCommand(
    string Title,
    string? Description)
    : IRequest<Result<List<string>>>;
