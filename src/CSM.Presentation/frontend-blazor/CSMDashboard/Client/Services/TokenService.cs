using Microsoft.JSInterop;

namespace CSMDashboard.Client.Services;

public class TokenService
{
    private readonly IJSRuntime _js;

    public TokenService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task<string> GetTokenAsync()
    {
        return await _js.InvokeAsync<string>("sessionStorage.getItem", "authToken");
    }

    public async Task RemoveTokenAsync()
    {
        await _js.InvokeVoidAsync("sessionStorage.removeItem", "authToken");
    }
}
