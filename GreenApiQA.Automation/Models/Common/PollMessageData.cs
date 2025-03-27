using System.Text.Json.Serialization;

namespace GreenApiQA.Automation.Models.Common;

public sealed record PollMessageData
{
    [JsonPropertyName("stanzaId")]
    public string? StanzaId { get; set; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("options")]
    public ICollection<Options>? Options { get; set; }
    
    [JsonPropertyName("votes")]
    public ICollection<Votes>? Votes { get; set; }
    
    [JsonPropertyName("multipleAnswers")]
    public bool MultipleAnswers { get; set; }
}