using AIWorkHub.Application.Features.Tags.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tags.CreateTag;

public sealed record CreateTagCommand(Guid ProjectId, CreateTagRequest Request)
    : IRequest<Result<TagResponse>>;
