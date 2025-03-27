using System.Net;
using System.Text;
using System.Text.Json;
using FluentAssertions;
using GreenApiQA.Automation.Config;
using GreenApiQA.Automation.Models.Logs;
using Xunit.Abstractions;

namespace GreenApiQA.Automation.Tests;

public sealed class GetChatHistoryTests
{
    private readonly HttpClient _client;
    
    private readonly GreenApiSettings _settings;
    
    private readonly ITestOutputHelper _output;
    
    public GetChatHistoryTests(ITestOutputHelper output)
    {
        _output   = output;
        _settings = SettingsLoader.Load();
        _client   = new HttpClient { BaseAddress = new Uri($"{_settings.BaseUrl}") };
    }

    [Theory]
    [InlineData(5)]
    public async Task GetChatHistory_ShouldReturnMessages(int count)
    {
        await Task.Delay(1000);
        
        var request = new GetChatHistoryRequest
        {
            ChatId = _settings.ChatId,
            Count = count,
        };
        
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync($"getChatHistory/{_settings.ApiTokenInstance}", content);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<IList<GetChatHistoryResponse>>(responseString);
        result?.Count.Should().Be(count);
    }
    
    public static IEnumerable<object[]> EmptyList()
    {
        yield return [ new Dictionary<string, object> { { "count", 5 } } ];
        yield return [ new Dictionary<string, object> { { "chatId", "11001004567@c.us" } } ];
        yield return [ new GetChatHistoryRequest { ChatId = "11001004567@c.us", Count = 0 } ];
        yield return [ new GetChatHistoryRequest { ChatId = "11001004567@c.us", Count = 5 } ];
    }

    [Theory]
    [MemberData(nameof(EmptyList))]
    public async Task GetHistory_ShouldReturnEmptyList(object request)
    {
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync($"getChatHistory/{_settings.ApiTokenInstance}", content);
        if (response.StatusCode == HttpStatusCode.TooManyRequests)
        {
            await Task.Delay(1000);
            response = await _client.PostAsync($"getChatHistory/{_settings.ApiTokenInstance}", content);
        }
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<IList<GetChatHistoryResponse>>(responseString);
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    public static IEnumerable<object[]> InvalidRequest()
    {
        yield return 
        [
            new Dictionary<string, object>
            {
                { "count", "test" }
            },
            "cannot unmarshal number",
            HttpStatusCode.BadRequest,
        ];
        
        yield return 
        [
            new Dictionary<string, object>
            {
                { "chatId", "123" }
            },
            "'chatId' must be one of the next formats: 'phone_number@c.us' or 'group_id@g.us'",
            HttpStatusCode.BadRequest,
        ];
    }
    
    [Theory]
    [MemberData(nameof(InvalidRequest))]
    public async Task GetHistory_ShouldReturnFailure(
        object request, 
        string expectedError,
        HttpStatusCode expectedStatusCode)
    {
        await Task.Delay(1000);
        
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync($"getChatHistory/{_settings.ApiTokenInstance}", content);
        response.StatusCode.Should().Be(expectedStatusCode);
        
        var responseString = await response.Content.ReadAsStringAsync();
        _output.WriteLine(responseString);
        responseString.Should().Contain(expectedError);
    }

    [Fact]
    public async Task GetChatHistory_ShouldReturnTooManyRequests()
    {
        var request = new GetChatHistoryRequest
        {
            ChatId = _settings.ChatId,
            Count = 5,
        };
        
        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        HttpResponseMessage? response = null;
        for (var i = 0; i < 5; i++)
        {
            response = await _client.PostAsync($"getChatHistory/{_settings.ApiTokenInstance}", content);
            if (response.StatusCode == HttpStatusCode.TooManyRequests) break;
            await Task.Delay(100);
        }
        
        response?.Should().NotBeNull();
        response?.StatusCode.Should().Be(HttpStatusCode.TooManyRequests);
    }
}