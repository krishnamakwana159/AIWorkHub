using AIWorkHub.Application.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.AI.SuggestPriority;

public sealed class SuggestPriorityHandler(
    IAIService aiService)
    : IRequestHandler<
        SuggestPriorityCommand,
        Result<string>>
{
    public async Task<Result<string>> Handle(
        SuggestPriorityCommand request,
        CancellationToken cancellationToken)
    {
        var priority =
            await aiService.SuggestPriorityAsync(
                request.Title,
                request.Description,
                cancellationToken);

        return Result<string>.Success(priority);
    }
}
