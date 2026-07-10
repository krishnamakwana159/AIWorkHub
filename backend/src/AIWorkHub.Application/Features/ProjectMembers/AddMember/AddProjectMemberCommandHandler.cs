using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.Domain.Entities;
using AIWorkHub.Domain.Enums;
using AIWorkHub.SharedKernel.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.ProjectMembers.AddMember;

public sealed class AddProjectMemberCommandHandler(
    IProjectRepository projectRepository,
    IUserRepository userRepository,
    IProjectMemberRepository memberRepository,
    INotificationService notificationService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddProjectMemberCommand, Result>
{
    public async Task<Result> Handle(
        AddProjectMemberCommand request,
        CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(
            request.ProjectId,
            cancellationToken);

        if (project is null)
            return Result.Failure("Project not found.");

        var user = await userRepository.GetByIdAsync(
            request.Request.UserId,
            cancellationToken);

        if (user is null)
            return Result.Failure("User not found.");

        if (await memberRepository.ExistsAsync(
            request.ProjectId,
            request.Request.UserId,
            cancellationToken))
        {
            return Result.Failure("User is already a member.");
        }

        var member = new ProjectMember
        {
            ProjectId = request.ProjectId,
            UserId = request.Request.UserId,
            Role = request.Request.Role
        };

        await memberRepository.AddAsync(member, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await notificationService.NotifyAsync(
            user.Id,
            "Added to Project",
            $"You were added to '{project.Name}'.",
            NotificationType.Info,
            $"/projects/{project.Id}",
            cancellationToken);

        return Result.Success();
    }
}
