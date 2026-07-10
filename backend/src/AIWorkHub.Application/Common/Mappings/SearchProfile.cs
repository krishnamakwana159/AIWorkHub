using AIWorkHub.Application.Features.Search.DTOs;
using AIWorkHub.Application.Features.Tasks.Attachments.DTOs;
using AIWorkHub.Application.Features.Tasks.Comments.DTOs;
using AIWorkHub.Application.Features.Tasks.DTOs;
using AIWorkHub.Domain.Entities;
using AutoMapper;

namespace AIWorkHub.Application.Common.Mappings;

public sealed class SearchProfile : Profile
{
    public SearchProfile()
    {
        CreateMap<Project, SearchProjectDto>()
            .ForMember(d => d.Status,
                o => o.MapFrom(s => s.Status.ToString()));

        CreateMap<WorkTask, SearchTaskDto>()
            .ForMember(d => d.Status,
                o => o.MapFrom(s => s.Status.ToString()));

        CreateMap<User, SearchUserDto>()
            .ForMember(d => d.FullName,
                o => o.MapFrom(s => $"{s.FirstName} {s.LastName}"));

        CreateMap<TaskComment, SearchCommentDto>()
            .ForMember(d => d.Comment,
                o => o.MapFrom(s => s.Comment));
    }
}
