using System.Text.Json.Serialization;

namespace GreenApiQA.Automation.Models.Send;

public sealed record SendMessageResponse
{
    [JsonPropertyName("idMessage")]
    public string? IdMessage { get; set; }
}