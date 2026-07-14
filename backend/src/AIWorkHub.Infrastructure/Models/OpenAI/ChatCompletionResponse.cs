using System.Text.Json.Serialization;

namespace AIWorkHub.Infrastructure.Models.OpenAI;

public sealed class ChatCompletionResponse
{
    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; set; } = new();
}

public sealed class Choice
{
    [JsonPropertyName("message")]
    public AssistantMessage Message { get; set; } = new();
}

public sealed class AssistantMessage
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}
