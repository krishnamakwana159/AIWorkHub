namespace AIWorkHub.Application.Interfaces;

public interface IAIService
{
    Task<string> GenerateTaskDescriptionAsync(
        string title,
        CancellationToken cancellationToken);

    Task<List<string>> GenerateTaskBreakdownAsync(
        string title,
        string? description,
        CancellationToken cancellationToken);

    Task<string> SummarizeProjectAsync(
        string projectName,
        string projectDescription,
        IEnumerable<string> tasks,
        CancellationToken cancellationToken);

    Task<string> SuggestPriorityAsync(
        string taskTitle,
        string? description,
        CancellationToken cancellationToken);
}
