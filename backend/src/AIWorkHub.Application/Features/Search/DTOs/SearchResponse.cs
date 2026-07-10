namespace AIWorkHub.Application.Features.Search.DTOs;

public sealed class SearchResponse
{
    public IReadOnlyList<SearchProjectDto> Projects { get; set; }
        = [];

    public IReadOnlyList<SearchTaskDto> Tasks { get; set; }
        = [];

    public IReadOnlyList<SearchUserDto> Users { get; set; }
        = [];

    public IReadOnlyList<SearchCommentDto> Comments { get; set; }
        = [];
}
