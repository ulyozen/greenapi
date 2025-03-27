using System.Text.Json.Serialization;

namespace GreenApiQA.Automation.Models.Common;

public sealed record Location
{
    [JsonPropertyName("nameLocation")]
    public string? NameLocation { get; set; }
    
    [JsonPropertyName("address")]
    public string? Address { get; set; }
    
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }
    
    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
    
    [JsonPropertyName("jpegThumbnail")]
    public string? JpegThumbnail { get; set; }
    
    [JsonPropertyName("isForwarded")]
    public bool IsForwarded { get; set; }
    
    [JsonPropertyName("forwardingScore")]
    public int ForwardingScore { get; set; }
}