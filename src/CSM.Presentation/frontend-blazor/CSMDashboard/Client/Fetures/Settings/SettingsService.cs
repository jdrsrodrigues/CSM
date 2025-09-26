using CSMDashboard.Client.Configuration;
using CSMDashboard.Shared.Models;
using System.Net.Http.Json;

namespace CSMDashboard.Client.Features.Settings;

public class SettingsService
{
    private readonly HttpClient _http;
    private readonly AppSettings _config;

    public SettingsService(HttpClient http, AppSettings config)
    {
        _http = http;
        _config = config;
    }

    public async Task<IEnumerable<SettingsPack>> GetSettingsAsync()
    {
        var url = $"{_config.ApiBaseUrl}/settings";
        return await _http.GetFromJsonAsync<IEnumerable<SettingsPack>>(url);
    }

    public string GetApiBaseUrl()
    {
        return _config.ApiBaseUrl;
    }
}

