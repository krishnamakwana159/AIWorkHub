using AIWorkHub.Application.Common.Interfaces;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.SharedKernel.Results;
using MediatR;

namespace AIWorkHub.Application.Features.AI.ProjectSummary;

public sealed class ProjectSummaryHandler(
    IProjectRepository projectRepository,
    IAIService aiService)
    : IRequestHandler<ProjectSummaryQuery, Result<string>>
{
    public async Task<Result<string>> Handle(
        ProjectSummaryQuery request,
        CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetProjectWithTasksAsync(
            request.ProjectId,
            cancellationToken);

        if (project is null)
        {
            return Result<string>.Failure("Project not found.");
        }

        var summary = await aiService.SummarizeProjectAsync(
            project.Name,
            project.Description ?? string.Empty,
            project.Tasks.Select(t =>
                $"{t.Title} - {t.Status}"),
            cancellationToken);

        return Result<string>.Success(summary);
    }
}
