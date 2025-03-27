using System.Text.Json.Serialization;

namespace GreenApiQA.Automation.Models.Send;

public sealed record BigPayload : SendMessageRequest
{
    [JsonPropertyName("payload1")]
    public string? Payload1 { get; set; }
    
    [JsonPropertyName("payload2")]
    public string? Payload2 { get; set; }
    
    [JsonPropertyName("payload3")]
    public string? Payload3 { get; set; }
}