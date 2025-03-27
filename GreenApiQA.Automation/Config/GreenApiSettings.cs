namespace GreenApiQA.Automation.Config;

public sealed class GreenApiSettings
{
    public required string ChatId { get; init; }
    
    public required string IdInstance { get; init; }
    
    public required string ApiTokenInstance { get; init; }
    
    public required string Url { get; init; }
    
    public string BaseUrl => $"{Url}/waInstance{IdInstance}/";
}