using CSMDashboard.Client.Configuration;
using CSMDashboard.Client.Features.Settings;
using CSMDashboard.Client.Fetures.Auth;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace CSMDashboard.Client.Features.Auth;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly IJSRuntime _js;
    SettingsService _settings;

    public AuthService(HttpClient http, IJSRuntime js, SettingsService settings)
    {
        _http = http;
        _js = js;
        _settings = settings;
    }

    public async Task<bool> LoginAsync(LoginDto loginDto)
    {
        var response = await _http.PostAsJsonAsync($"{_settings.GetApiBaseUrl()}/auth/login", loginDto);

        if (!response.IsSuccessStatusCode)
            return false;

        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        await _js.InvokeVoidAsync("sessionStorage.setItem", "authToken", result.Token);
        return true;
    }

    private class TokenResponse
    {
        public string Token { get; set; }
    }
}

