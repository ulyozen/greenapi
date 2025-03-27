using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using GreenApiQA.Automation.Config;
using GreenApiQA.Automation.Models.Send;
using Xunit.Abstractions;

namespace GreenApiQA.Automation.Tests;

public sealed class SendMessageTests
{
    private readonly HttpClient _client;
    
    private readonly GreenApiSettings _settings;
    
    private readonly ITestOutputHelper _output;
    
    public SendMessageTests(ITestOutputHelper output)
    {
        _output   = output;
        _settings = SettingsLoader.Load();
        _client   = new HttpClient { BaseAddress = new Uri($"{_settings.BaseUrl}") };
    }
    
    [Theory]
    [MemberData(nameof(ValidMessages))]
    public async Task SendMessage_ShouldReturnSuccess(string message)
    {
        var request = new SendMessageRequest
        {
            ChatId  = _settings.ChatId,
            Message = message
        };
        
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync($"sendMessage/{_settings.ApiTokenInstance}", content);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var responseString = await response.Content.ReadAsStringAsync();
        responseString.Should().Contain("idMessage");
    }
    
    [Fact]
    public async Task SendMessage_WithQuotedMessageId_ShouldReturnSuccess()
    {
        var request = new SendMessageRequest
        {
            ChatId  = _settings.ChatId,
            Message = "Hello from Green API",
        };
        
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync($"sendMessage/{_settings.ApiTokenInstance}", content);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var responseString = await response.Content.ReadAsStringAsync();
        responseString.Should().Contain("idMessage");
        
        var result = JsonSerializer.Deserialize<SendMessageResponse>(responseString);
        _output.WriteLine($"idMessage: {result?.IdMessage}");
        
        var contentWithQuoted = new StringContent(
            JsonSerializer.Serialize(request with { Message = "Hello from Quoted", QuotedMessageId = result?.IdMessage } ), 
            Encoding.UTF8, 
            "application/json");
        
        var responseWithQuoted = await _client.PostAsync($"sendMessage/{_settings.ApiTokenInstance}", contentWithQuoted);
        responseWithQuoted.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Theory]
    [MemberData(nameof(InvalidRequest))]
    public async Task SendInvalidRequest_ShouldReturnFailure(
        object request, 
        string expectedError, 
        HttpStatusCode expectedStatusCode)
    {
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync($"sendMessage/{_settings.ApiTokenInstance}", content);
        response.StatusCode.Should().Be(expectedStatusCode);
        
        var responseString = await response.Content.ReadAsStringAsync();
        _output.WriteLine(responseString);
        responseString.Should().Contain(expectedError);
    }
    
    public static IEnumerable<object[]> ValidMessages()
    {
        yield return ["   "];
        yield return ["Hello from Green API"];
        yield return ["Hello from Emoji \ud83d\ude0a"];
        yield return ["https://green-api.com"];
        yield return [new string('A', 1)];
        yield return [new string('A', 20000)];
    }
    
    public static IEnumerable<object[]> InvalidRequest()
    {
        var request = new SendMessageRequest();
        yield return 
        [
            request with { ChatId = "123", Message = "test" }, 
            "'chatId' must be one of the next formats: 'phone_number@c.us' or 'group_id@g.us",
            HttpStatusCode.BadRequest
        ];
        
        yield return 
        [
            request with { ChatId = string.Empty, Message = "test" },
            "'chatId' is not allowed to be empty",
            HttpStatusCode.BadRequest
        ];
        
        yield return 
        [
            request with { ChatId = "11001234567@c.us", Message = string.Empty },
            "'message' is not allowed to be empty",
            HttpStatusCode.BadRequest
        ];
        
        yield return 
        [
            request with { ChatId = null, Message = "test" },
            "'chatId' must be a string",
            HttpStatusCode.BadRequest
        ];
        
        yield return 
        [
            request with { ChatId = "11001234567@c.us", Message = null },
            "'message' must be a string",
            HttpStatusCode.BadRequest
        ];
        
        yield return 
        [
            request with { ChatId = "11001234567@c.us", Message = new string('A', 20001) },
            "'message' length must be less than or equal to 20000 characters long",
            HttpStatusCode.BadRequest
        ];
        
        yield return 
        [
            request with { ChatId = "11001234567@c.us", Message = "test", QuotedMessageId = "short" },
            "'message id' must be at least 16 symbols",
            HttpStatusCode.BadRequest
        ];
        
        yield return 
        [
            new Dictionary<string, string> { { "chatId", "11001234567@c.us" } },
            "'message' is required",
            HttpStatusCode.BadRequest
        ];
        
        yield return 
        [
            new Dictionary<string, string> { { "message", "test" } },
            "'chatId' is required",
            HttpStatusCode.BadRequest
        ];
        
        yield return 
        [
            new Dictionary<string, string>
            {
                { "chatId", "11001234567@c.us" },
                { "message", "test" },
                { "linkPreview", "test" },
            },
            "'linkPreview' must be a boolean",
            HttpStatusCode.BadRequest
        ];

        var bigMessage = new string('X', 50000);
        yield return
        [
            new BigPayload
            {
                ChatId   = "11001234567@c.us",
                Message  = "hello",
                Payload1 = bigMessage,
                Payload2 = bigMessage,
                Payload3 = bigMessage
            },
            "request entity too large",
            HttpStatusCode.InternalServerError
        ];
    }
}