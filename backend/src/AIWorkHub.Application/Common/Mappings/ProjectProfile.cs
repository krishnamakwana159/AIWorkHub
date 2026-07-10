using AIWorkHub.Application.Features.ProjectMembers.DTOs;
using AIWorkHub.Application.Features.Projects.DTOs;
using AIWorkHub.Domain.Entities;
using AutoMapper;

namespace AIWorkHub.Application.Common.Mappings;

public sealed class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectResponse>();

        CreateMap<CreateProjectRequest, Project>();

        CreateMap<UpdateProjectRequest, Project>();

        CreateMap<ProjectMember, ProjectMemberResponse>()
            .ForMember(
                d => d.FullName,
                o => o.MapFrom(s => $"{s.User.FirstName} {s.User.LastName}"))
            .ForMember(
                d => d.Email,
                o => o.MapFrom(s => s.User.Email));
    }
}
