using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Tags.DTOs;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tags.CreateTag;

public sealed class CreateTagCommandHandler(
    IProjectRepository projectRepository,
    ITagRepository tagRepository,
    IProjectMemberRepository memberRepository,
    ICurrentUserService currentUser,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateTagCommand, Result<TagResponse>>
{
    public async Task<Result<TagResponse>> Handle(
        CreateTagCommand request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.UserId, out var userId))
            return Result<TagResponse>.Failure("Unauthorized.");

        var project = await projectRepository.GetByIdAsync(
            request.ProjectId,
            cancellationToken);

        if (project is null)
            return Result<TagResponse>.Failure("Project not found.");

        var isOwner = project.OwnerId == userId;

        var isMember = !isOwner && await memberRepository.ExistsAsync(
            request.ProjectId,
            userId,
            cancellationToken);

        if (!isOwner && !isMember)
            return Result<TagResponse>.Failure("You do not have access to this project.");

        var name = request.Request.Name.Trim();

        var existing = await tagRepository.GetByNameAsync(
            request.ProjectId,
            name,
            cancellationToken);

        if (existing is not null)
            return Result<TagResponse>.Failure("A tag with this name already exists.");

        var tag = new Domain.Entities.Tag
        {
            ProjectId = request.ProjectId,
            Name = name,
            Color = request.Request.Color
        };

        await tagRepository.AddAsync(tag, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<TagResponse>.Success(new TagResponse
        {
            Id = tag.Id,
            Name = tag.Name,
            Color = tag.Color
        });
    }
}
