using System.Text.Json.Serialization;

namespace GreenApiQA.Automation.Models.Common;

public sealed record QuotedMessage
{
    [JsonPropertyName("stanzaId")]
    public string? StanzaId { get; set; }
    
    [JsonPropertyName("participant")]
    public string? Participant { get; set; }
    
    [JsonPropertyName("typeMessage")]
    public string? TypeMessage { get; set; }
}