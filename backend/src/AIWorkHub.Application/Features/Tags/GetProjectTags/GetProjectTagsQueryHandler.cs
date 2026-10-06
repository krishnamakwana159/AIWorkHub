using AIWorkHub.Application.Features.Tags.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tags.GetProjectTags;

public sealed class GetProjectTagsQueryHandler(
    ITagRepository tagRepository)
    : IRequestHandler<GetProjectTagsQuery, Result<List<TagResponse>>>
{
    public async Task<Result<List<TagResponse>>> Handle(
        GetProjectTagsQuery request,
        CancellationToken cancellationToken)
    {
        var tags = await tagRepository.GetByProjectIdAsync(
            request.ProjectId,
            cancellationToken);

        return Result<List<TagResponse>>.Success(
            tags.Select(x => new TagResponse
            {
                Id = x.Id,
                Name = x.Name,
                Color = x.Color
            }).ToList());
    }
}
