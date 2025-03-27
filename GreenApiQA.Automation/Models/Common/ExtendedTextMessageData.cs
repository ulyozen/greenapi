using System.Text.Json.Serialization;

namespace GreenApiQA.Automation.Models.Common;

public sealed record ExtendedTextMessageData
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }
}