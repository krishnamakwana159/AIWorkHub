using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.ProjectMembers.RemoveMember;

public sealed class RemoveProjectMemberCommandHandler(
    IProjectRepository projectRepository,
    IProjectMemberRepository memberRepository,
    INotificationService notificationService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RemoveProjectMemberCommand, Result>
{
    public async Task<Result> Handle(
        RemoveProjectMemberCommand request,
        CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(
            request.ProjectId,
            cancellationToken);

        if (project is null)
            return Result.Failure("Project not found.");

        var member = await memberRepository.GetAsync(
            request.ProjectId,
            request.UserId,
            cancellationToken);

        if (member is null)
            return Result.Failure("This user is not a member of the project.");

        if (member.Role == ProjectRole.Owner)
            return Result.Failure("The project owner cannot be removed.");

        memberRepository.Remove(member);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await notificationService.NotifyAsync(
            request.UserId,
            "Removed from Project",
            $"You were removed from '{project.Name}'.",
            NotificationType.Info,
            null,
            cancellationToken);

        return Result.Success();
    }
}
