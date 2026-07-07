using AIWorkHub.Domain.Enums;

namespace AIWorkHub.Application.Features.Tasks.DTOs;

public sealed class UpdateTaskStatusRequest
{
    public Domain.Enums.TaskStatus Status { get; set; }
}
