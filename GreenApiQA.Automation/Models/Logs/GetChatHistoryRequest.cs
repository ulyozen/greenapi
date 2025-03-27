using System.Text.Json.Serialization;

namespace GreenApiQA.Automation.Models.Logs;

public sealed record GetChatHistoryRequest
{
    [JsonPropertyName("chatId")]
    public string? ChatId { get; set; }
    
    [JsonPropertyName("count")]
    public int Count { get; set; }
}