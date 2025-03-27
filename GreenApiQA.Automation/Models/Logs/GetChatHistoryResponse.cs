using System.Text.Json.Serialization;
using GreenApiQA.Automation.Models.Common;

namespace GreenApiQA.Automation.Models.Logs;

public sealed record GetChatHistoryResponse
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    
    [JsonPropertyName("idMessage")]
    public string? IdMessage { get; set; }
    
    [JsonPropertyName("timestamp")]
    public int Timestamp { get; set; }
    
    [JsonPropertyName("statusMessage")]
    public string? StatusMessage { get; set; }
    
    [JsonPropertyName("sendByApi")]
    public bool SendByApi { get; set; }
    
    [JsonPropertyName("typeMessage")]
    public string? TypeMessage { get; set; }
    
    [JsonPropertyName("chatId")]
    public string? ChatId { get; set; }
    
    [JsonPropertyName("senderId")]
    public string? SenderId { get; set; }
    
    [JsonPropertyName("senderName")]
    public string? SenderName { get; set; }
    
    [JsonPropertyName("senderContactName")]
    public string? SenderContactName { get; set; }
    
    [JsonPropertyName("isForwarded")]
    public bool IsForwarded { get; set; }
    
    [JsonPropertyName("forwardingScore")]
    public int ForwardingScore { get; set; }
    
    [JsonPropertyName("textMessage")]
    public string? TextMessage { get; set; }
    
    [JsonPropertyName("downloadUrl")]
    public string? DownloadUrl { get; set; }
    
    [JsonPropertyName("caption")]
    public string? Caption { get; set; }
    
    [JsonPropertyName("fileName")]
    public string? FileName { get; set; }
    
    [JsonPropertyName("jpegThumbnail")]
    public string? JpegThumbnail { get; set; }
    
    [JsonPropertyName("mimeType")]
    public string? MimeType { get; set; }
    
    [JsonPropertyName("isAnimated")]
    public bool IsAnimated { get; set; }
    
    [JsonPropertyName("location")]
    public Location? Location { get; set; }
    
    [JsonPropertyName("contact")]
    public Contact? Contact { get; set; }
    
    [JsonPropertyName("extendedTextMessage")]
    public ExtendedTextMessage? ExtendedTextMessage { get; set; }
    
    [JsonPropertyName("extendedTextMessageData")]
    public ExtendedTextMessageData? ExtendedTextMessageData { get; set; }
    
    [JsonPropertyName("pollMessageData")]
    public PollMessageData? PollMessageData { get; set; }
    
    [JsonPropertyName("quotedMessage")]
    public QuotedMessage? QuotedMessage { get; set; }
    
    [JsonPropertyName("deletedMessageId")]
    public string? DeletedMessageId { get; set; }
    
    [JsonPropertyName("editedMessageId")]
    public string? EditedMessageId { get; set; }
    
    [JsonPropertyName("isEdited")]
    public bool IsEdited { get; set; }
    
    [JsonPropertyName("isDeleted")]
    public bool IsDeleted { get; set; }
}