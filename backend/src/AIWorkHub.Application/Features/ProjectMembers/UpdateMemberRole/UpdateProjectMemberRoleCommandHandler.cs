using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.ProjectMembers.UpdateMemberRole;

public sealed class UpdateProjectMemberRoleCommandHandler(
    IProjectRepository projectRepository,
    IProjectMemberRepository memberRepository,
    INotificationService notificationService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateProjectMemberRoleCommand, Result>
{
    public async Task<Result> Handle(
        UpdateProjectMemberRoleCommand request,
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
            return Result.Failure("The project owner's role cannot be changed.");

        if (request.Request.Role == ProjectRole.Owner)
            return Result.Failure("Ownership cannot be transferred from this endpoint.");

        member.Role = request.Request.Role;

        memberRepository.Update(member);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await notificationService.NotifyAsync(
            request.UserId,
            "Project Role Updated",
            $"Your role on '{project.Name}' is now {request.Request.Role}.",
            NotificationType.Info,
            $"/projects/{project.Id}",
            cancellationToken);

        return Result.Success();
    }
}
