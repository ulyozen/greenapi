using System.Text.Json.Serialization;

namespace GreenApiQA.Automation.Models.Common;

public sealed record Options
{
    [JsonPropertyName("optionName")]
    public string? OptionName { get; set; }
}