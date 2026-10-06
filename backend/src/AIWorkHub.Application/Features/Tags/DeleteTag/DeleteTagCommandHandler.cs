using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Tags.DeleteTag;

public sealed class DeleteTagCommandHandler(
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteTagCommand, Result>
{
    public async Task<Result> Handle(
        DeleteTagCommand request,
        CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByIdAsync(
            request.TagId,
            cancellationToken);

        if (tag is null)
            return Result.Failure("Tag not found.");

        tagRepository.Remove(tag);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
