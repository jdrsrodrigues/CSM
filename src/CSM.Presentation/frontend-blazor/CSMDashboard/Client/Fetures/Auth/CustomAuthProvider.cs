using CSMDashboard.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CSMDashboard.Client.Fetures.Auth;
public class CustomAuthProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    private readonly TokenService _tokenService;
    public CustomAuthProvider(TokenService tokenService)
    {
        _tokenService = tokenService;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _tokenService.GetTokenAsync();

        if (string.IsNullOrEmpty(token))
            return new AuthenticationState(_anonymous);

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Usuário"),
            new Claim(ClaimTypes.Role, "Admin") // ou o papel real
        }, "jwt");

        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }
    public void NotifyUserAuthentication(string token)
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Usuário"),
            new Claim(ClaimTypes.Role, "Admin")
        }, "jwt");

        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

}
