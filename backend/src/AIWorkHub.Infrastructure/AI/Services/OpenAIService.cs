using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AIWorkHub.Application.Interfaces;
using AIWorkHub.Infrastructure.Configuration;
using AIWorkHub.Infrastructure.Models.OpenAI;
using Microsoft.Extensions.Options;

namespace AIWorkHub.Infrastructure.AI.Services;

public sealed class OpenAIService(
    HttpClient httpClient,
    IOptions<OpenAISettings> options)
    : IAIService
{
    private readonly OpenAISettings _settings = options.Value;

    public async Task<string> GenerateTaskDescriptionAsync(
        string title,
        CancellationToken cancellationToken)
    {
        var request = new ChatCompletionRequest
        {
            Model = _settings.Model,
            Messages =
            [
                new Message
                {
                    Role = "system",
                    Content =
                        """
                        You are an experienced project manager.

                        Generate a professional task description.

                        Return only the description.
                        """
                },

                new Message
                {
                    Role = "user",
                    Content = $"Task: {title}"
                }
            ]
        };

        var response = await SendAsync(request, cancellationToken);

        return response
            .Choices
            .FirstOrDefault()?
            .Message
            .Content
            .Trim()
            ?? string.Empty;
    }

    public async Task<List<string>> GenerateTaskBreakdownAsync(
        string title,
        string? description,
        CancellationToken cancellationToken)
    {
        var request = new ChatCompletionRequest
        {
            Model = _settings.Model,
            Messages =
            [
                new Message
                {
                    Role = "system",
                    Content =
                        """
                        Break the task into a numbered list.
                        Return one item per line.
                        """
                },

                new Message
                {
                    Role = "user",
                    Content =
                        $"Title: {title}\nDescription: {description}"
                }
            ]
        };

        var response = await SendAsync(request, cancellationToken);

        var text = response.Choices.First().Message.Content;

        return text
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim())
            .ToList();
    }

    public async Task<string> SuggestPriorityAsync(
        string title,
        string? description,
        CancellationToken cancellationToken)
    {
        var request = new ChatCompletionRequest
        {
            Model = _settings.Model,
            Messages =
            [
                new Message
                {
                    Role = "system",
                    Content =
                        """
                        Suggest ONLY one priority.

                        Allowed values:

                        Low
                        Medium
                        High
                        Critical
                        """
                },

                new Message
                {
                    Role = "user",
                    Content =
                        $"Title:{title}\nDescription:{description}"
                }
            ]
        };

        var response = await SendAsync(request, cancellationToken);

        return response
            .Choices
            .First()
            .Message
            .Content
            .Trim();
    }

    public async Task<string> SummarizeProjectAsync(
        string projectName,
        string projectDescription,
        IEnumerable<string> tasks,
        CancellationToken cancellationToken)
    {
        var request = new ChatCompletionRequest
        {
            Model = _settings.Model,
            Messages =
            [
                new Message
                {
                    Role = "system",
                    Content =
                        """
                        You are an experienced project manager.

                        Generate a concise professional project summary.
                        """
                },

                new Message
                {
                    Role = "user",
                    Content =
                        $"""
                        Project: {projectName}

                        Description:
                        {projectDescription}

                        Tasks:
                        {string.Join(Environment.NewLine, tasks)}
                        """
                }
            ]
        };

        var response = await SendAsync(request, cancellationToken);

        return response
            .Choices
            .FirstOrDefault()?
            .Message
            .Content
            .Trim()
            ?? string.Empty;
    }
    private async Task<ChatCompletionResponse> SendAsync(
        ChatCompletionRequest request,
        CancellationToken cancellationToken)
    {
        using var message = new HttpRequestMessage(
            HttpMethod.Post,
            "https://api.openai.com/v1/chat/completions");

        message.Headers.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                _settings.ApiKey);

        message.Content =
            new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

        using var response =
            await httpClient.SendAsync(
                message,
                cancellationToken);

        response.EnsureSuccessStatusCode();

        var json =
            await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<ChatCompletionResponse>(
                   json,
                   new JsonSerializerOptions
                   {
                       PropertyNameCaseInsensitive = true
                   })!
               ?? throw new InvalidOperationException("Invalid OpenAI response.");
    }
}
