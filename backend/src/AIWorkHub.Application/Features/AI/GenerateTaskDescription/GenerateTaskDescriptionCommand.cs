using AIWorkHub.Application.Features.AI.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.AI.GenerateTaskDescription;

public sealed record GenerateTaskDescriptionCommand(
    GenerateTaskDescriptionRequest Request)
    : IRequest<Result<GenerateTaskDescriptionResponse>>;
