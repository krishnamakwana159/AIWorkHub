namespace AIWorkHub.Application.Features.Tags.DTOs;

public sealed class CreateTagRequest
{
    public string Name { get; set; } = string.Empty;

    public string Color { get; set; } = "#64748b";
}
