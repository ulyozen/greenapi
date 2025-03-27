using Microsoft.Extensions.Configuration;

namespace GreenApiQA.Automation.Config;

public static class SettingsLoader
{
    public static GreenApiSettings Load()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        
        var settings = config.GetSection("GreenApi").Get<GreenApiSettings>();
        
        if (string.IsNullOrWhiteSpace(settings?.ChatId)          ||
            string.IsNullOrWhiteSpace(settings.IdInstance)       ||
            string.IsNullOrWhiteSpace(settings.ApiTokenInstance) ||
            string.IsNullOrWhiteSpace(settings.Url))
            throw new Exception("GreenAPI settings are missing.");
        
        return settings;
    }
}