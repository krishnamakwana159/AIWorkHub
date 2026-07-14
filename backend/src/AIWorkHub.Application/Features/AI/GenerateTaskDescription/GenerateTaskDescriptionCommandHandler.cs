using AIWorkHub.Application.Features.AI.DTOs;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.AI.GenerateTaskDescription;

public sealed class GenerateTaskDescriptionCommandHandler(
    IAIService aiService)
    : IRequestHandler<
        GenerateTaskDescriptionCommand,
        Result<GenerateTaskDescriptionResponse>>
{
    public async Task<Result<GenerateTaskDescriptionResponse>> Handle(
        GenerateTaskDescriptionCommand request,
        CancellationToken cancellationToken)
    {
        var description =
            await aiService.GenerateTaskDescriptionAsync(
                request.Request.Title,
                cancellationToken);

        return Result<GenerateTaskDescriptionResponse>.Success(
            new GenerateTaskDescriptionResponse
            {
                Description = description
            });
    }
}
