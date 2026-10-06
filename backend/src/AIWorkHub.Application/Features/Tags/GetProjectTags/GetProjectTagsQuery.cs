using AIWorkHub.Application.Features.Tags.DTOs;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tags.GetProjectTags;

public sealed record GetProjectTagsQuery(Guid ProjectId)
    : IRequest<Result<List<TagResponse>>>;
