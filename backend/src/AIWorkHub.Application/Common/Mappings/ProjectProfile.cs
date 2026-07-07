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
    }
}
