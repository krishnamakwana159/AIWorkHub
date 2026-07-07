using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.Delete;

public sealed class DeleteProjectCommandHandler(
    IProjectRepository repository,
    ICurrentUserService currentUser,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteProjectCommand, Result>
{
    public async Task<Result> Handle(
        DeleteProjectCommand request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(currentUser.UserId, out var ownerId))
            return Result.Failure("Unauthorized.");

        var project = await repository.GetByIdWithOwnerAsync(
            request.Id,
            ownerId,
            cancellationToken);

        if (project is null)
            return Result.Failure("Project not found.");

        repository.Remove(project);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
