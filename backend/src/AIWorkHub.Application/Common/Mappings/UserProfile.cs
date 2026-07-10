using AIWorkHub.Application.Features.Notifications.DTOs;
using AIWorkHub.Domain.Entities;
using AutoMapper;

namespace AIWorkHub.Application.Common.Mappings;

public sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Notification, NotificationResponse>();
    }
}
