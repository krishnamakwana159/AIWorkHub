using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Features.Search.DTOs;
using AIWorkHub.Application.Features.Search.Specifications;
using AIWorkHub.Application.Interfaces.Repositories;
using AIWorkHub.SharedKernel.Results;
using AutoMapper;
using MediatR;

namespace AIWorkHub.Application.Features.Search.GlobalSearch;

public sealed class SearchQueryHandler(
    IProjectRepository projectRepository,
    IWorkTaskRepository taskRepository,
    IUserRepository userRepository,
    ITaskCommentRepository commentRepository,
    IMapper mapper)
    : IRequestHandler<SearchQuery, Result<SearchResponse>>
{
    public async Task<Result<SearchResponse>> Handle(
        SearchQuery request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Query))
        {
            return Result<SearchResponse>.Success(new SearchResponse());
        }

        var projects = await projectRepository.ListAsync(
            new ProjectSearchSpecification(request.Query),
            cancellationToken);

        var tasks = await taskRepository.ListAsync(
            new TaskSearchSpecification(request.Query),
            cancellationToken);

        var users = await userRepository.ListAsync(
            new UserSearchSpecification(request.Query),
            cancellationToken);

        var comments = await commentRepository.ListAsync(
            new CommentSearchSpecification(request.Query),
            cancellationToken);

        var response = new SearchResponse
        {
            Projects = mapper.Map<IReadOnlyList<SearchProjectDto>>(projects),
            Tasks = mapper.Map<IReadOnlyList<SearchTaskDto>>(tasks),
            Users = mapper.Map<IReadOnlyList<SearchUserDto>>(users),
            Comments = mapper.Map<IReadOnlyList<SearchCommentDto>>(comments)
        };

        return Result<SearchResponse>.Success(response);
    }
}
