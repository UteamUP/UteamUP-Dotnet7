using Blazor.SubtleCrypto;
using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UteamUP.Client.Web.Repository.Implementations;
using UteamUP.Client.Web.Repository.Interfaces;
using UteamUP.Client.Web.Shared;
using UteamUP.Shared.States;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var currentAssembly = typeof(Program).Assembly;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add Base Component Services
builder.Services.TryAddScoped<AppState>();

// Add Repositories and Services
builder.Services.TryAddScoped<ITenantWebRepository, TenantWebRepository>();
builder.Services.TryAddScoped<IUserRepository, UserRepository>();
builder.Services.TryAddScoped<IPlanRepository, PlanRepository>();
builder.Services.TryAddScoped<IHeaderRepository, HeaderRepository>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.TryAddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("UteamUP.ServerAPI"));

// Add Local Session Storage
builder.Services.AddBlazoredLocalStorage(config => 
{
    config.JsonSerializerOptions.WriteIndented = true;
});

// Add Blazor Toast for Error Messages
builder.Services.AddBlazoredToast();

// Add SubleCrypto for Hashing
builder.Services.AddSubtleCrypto(opt => 
        opt.Key = "HpDNdZssfHeUevF62RHAHtLUX3iFDjz7Lcezck8cXLWrUf6CeqNcgDERbeYfxVmH" //Use another key
);

// Configure MSAL authentication
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add(builder.Configuration.GetSection("ServerApi")["Scopes"]);
});

builder.Services.AddMemoryCache();


builder.Services.AddHttpClient("UteamUP.ServerAPI",
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});


// Add this line to add the IAccessTokenProvider service
builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddApiAuthorization()
    .AddAccountClaimsPrincipalFactory<AccountClaimsPrincipalFactory<RemoteUserAccount>>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazorStrap();
builder.Services.AddBlazoredModal();
builder.Services.AddAutoMapper(currentAssembly);

#if DEBUG
builder.Services.AddSassCompiler();
#endif

await builder.Build().RunAsync();