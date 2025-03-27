using System.Text.Json.Serialization;

namespace GreenApiQA.Automation.Models.Common;

public sealed record ExtendedTextMessage
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonPropertyName("previewType")]
    public string? PreviewType { get; set; }
    
    [JsonPropertyName("jpegThumbnail")]
    public string? JpegThumbnail { get; set; }
    
    [JsonPropertyName("stanzaId")]
    public string? StanzaId { get; set; }
    
    [JsonPropertyName("participant")]
    public string? Participant { get; set; }
    
    [JsonPropertyName("isForwarded")]
    public bool IsForwarded { get; set; }
    
    [JsonPropertyName("forwardingScore")]
    public int ForwardingScore { get; set; }
}