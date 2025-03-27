using System.Text.Json.Serialization;

namespace GreenApiQA.Automation.Models.Send;

public record SendMessageRequest
{
    [JsonPropertyName("chatId")]
    public string? ChatId { get; set; }
    
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    
    [JsonPropertyName("quotedMessageId")]
    public string? QuotedMessageId { get; set; }
    
    [JsonPropertyName("linkPreview")]
    public bool LinkPreview { get; set; }
}