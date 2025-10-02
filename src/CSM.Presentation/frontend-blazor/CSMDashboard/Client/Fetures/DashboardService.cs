using CSMDashboard.Client.Features.Settings;
using CSMDashboard.Shared.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CSMDashboard.Client.Fetures;
public class DashboardService
{
    private readonly HttpClient _http;
    private readonly SettingsService _settings;

    public DashboardService(HttpClient http, SettingsService settings)
    {
        _http = http;
        _settings = settings;
    }

    public async Task<DashboardResumo> GetResumoAsync(string token)
    {
        var url = $"{_settings.GetApiBaseUrl()}/dashboard/resumo";
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await _http.GetFromJsonAsync<DashboardResumo>(url);
    }
}
