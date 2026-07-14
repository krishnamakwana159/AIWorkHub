using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.AI.SuggestPriority;

public sealed record SuggestPriorityCommand(
    string Title,
    string? Description)
    : IRequest<Result<string>>;
