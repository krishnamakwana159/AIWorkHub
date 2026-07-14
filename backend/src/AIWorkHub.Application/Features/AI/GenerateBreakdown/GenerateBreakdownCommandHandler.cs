using AIWorkHub.Application.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.AI.GenerateBreakdown;

public sealed class GenerateBreakdownHandler(
    IAIService aiService)
    : IRequestHandler<
        GenerateBreakdownCommand,
        Result<List<string>>>
{
    public async Task<Result<List<string>>> Handle(
        GenerateBreakdownCommand request,
        CancellationToken cancellationToken)
    {
        var tasks =
            await aiService.GenerateTaskBreakdownAsync(
                request.Title,
                request.Description,
                cancellationToken);

        return Result<List<string>>.Success(tasks);
    }
}
