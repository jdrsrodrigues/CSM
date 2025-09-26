using CSMDashboard.Client;
using CSMDashboard.Client.Configuration;
using CSMDashboard.Client.Features.Alertas;
using CSMDashboard.Client.Features.Auth;
using CSMDashboard.Client.Features.Settings;
using CSMDashboard.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//carrega o appsettings e injeta ele como serviço
builder.Services.AddScoped(sp =>
{
    var http = sp.GetRequiredService<HttpClient>();
    return http.GetFromJsonAsync<AppSettings>("appsettings.json").Result;
});


builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<SettingsService>();
builder.Services.AddScoped<AlertasService>();

await builder.Build().RunAsync();
