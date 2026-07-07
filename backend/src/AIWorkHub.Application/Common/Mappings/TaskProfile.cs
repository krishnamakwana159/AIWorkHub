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
    }
}
