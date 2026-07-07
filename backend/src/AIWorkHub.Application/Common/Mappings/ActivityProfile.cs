using AIWorkHub.Application.Features.Activities.DTOs;
using AIWorkHub.Domain.Entities;
using AutoMapper;

namespace AIWorkHub.Application.Common.Mappings;

public sealed class ActivityProfile : Profile
{
    public ActivityProfile()
    {
        CreateMap<ActivityLog, ActivityResponse>();
    }
}
