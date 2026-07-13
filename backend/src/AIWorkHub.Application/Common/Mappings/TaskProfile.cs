using AIWorkHub.Application.Features.Kanban.DTOs;
using AIWorkHub.Application.Features.Tasks.Attachments.DTOs;
using AIWorkHub.Application.Features.Tasks.Comments.DTOs;
using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.Domain.Entities;
using AutoMapper;

namespace AIWorkHub.Application.Common.Mappings;

public sealed class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<WorkTask, TaskResponse>();

        CreateMap<CreateTaskRequest, WorkTask>();

        CreateMap<UpdateTaskRequest, WorkTask>();

        CreateMap<TaskComment, CommentResponse>()
            .ForMember(d => d.UserName,
                o => o.MapFrom(s => s.User.FirstName + " " + s.User.LastName));

        CreateMap<TaskAttachment, AttachmentResponse>()
            .ForMember(
                d => d.UploadedBy,
                opt => opt.MapFrom(x =>
                    $"{x.UploadedByUser.FirstName} {x.UploadedByUser.LastName}".Trim()))
            .ForMember(
                d => d.UploadedAtUtc,
                opt => opt.MapFrom(x => x.CreatedAtUtc));

        CreateMap<WorkTask, KanbanTaskDto>()
            .ForMember(
                d => d.AssignedUserName,
                o => o.MapFrom(s =>
                    s.AssignedUser == null
                        ? null
                        : $"{s.AssignedUser.FirstName} {s.AssignedUser.LastName}"));
    }
}
