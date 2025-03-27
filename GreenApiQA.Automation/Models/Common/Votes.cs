using System.Text.Json.Serialization;

namespace GreenApiQA.Automation.Models.Common;

public sealed record Votes
{
    [JsonPropertyName("optionName")]
    public string? OptionName { get; set; }
    
    [JsonPropertyName("optionVoters")]
    public ICollection<string>? OptionVoters { get; set; }
}