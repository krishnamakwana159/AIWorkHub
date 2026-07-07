using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.Projects.Restore;

public sealed class RestoreProjectCommandHandler(
    IProjectRepository repository,
    ICurrentUserService currentUser,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RestoreProjectCommand, Result>
{
    public async Task<Result> Handle(
        RestoreProjectCommand request,
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

        project.IsArchived = false;

        repository.Update(project);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
