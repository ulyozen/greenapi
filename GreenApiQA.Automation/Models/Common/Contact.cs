using System.Text.Json.Serialization;

namespace GreenApiQA.Automation.Models.Common;

public sealed record Contact
{
    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; }
    
    [JsonPropertyName("vcard")]
    public string? Vcard { get; set; }
    
    [JsonPropertyName("isForwarded")]
    public bool IsForwarded { get; set; }
    
    [JsonPropertyName("forwardingScore")]
    public int ForwardingScore { get; set; }
}