using CSMDashboard.Client.Features.Settings;
using CSMDashboard.Shared.Models;
using System.Net.Http.Json;

namespace CSMDashboard.Client.Features.Alertas;

public class AlertasService
{
    private readonly HttpClient _http;
    private readonly SettingsService _settings;

    public AlertasService(HttpClient http, SettingsService settings)
    {
        _http = http;
        _settings = settings;
    }

    public async Task<IEnumerable<Alerta>> GetAlertasAsync(string token)
    {
        var url = $"{_settings.GetApiBaseUrl()}/alertas";
        _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        return await _http.GetFromJsonAsync<IEnumerable<Alerta>>(url);
    }
}
