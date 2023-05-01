using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UteamUP.Client.Middleware;
using UteamUP.Client.Web.Repository.Implementations;
using UteamUP.Client.Web.Repository.Interfaces;
using UteamUP.Client.Web.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var currentAssembly = typeof(Program).Assembly;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add Base Component Services
builder.Services.TryAddScoped<InitComponent>();
builder.Services.TryAddScoped<GlobalState>();
builder.Services.TryAddScoped<UserComponentBase>();
builder.Services.TryAddScoped<TenantsComponentBase>();
builder.Services.TryAddScoped<AuthorizationComponentBase>();
builder.Services.TryAddScoped<GlobalAppState>();

// Add Repositories and Services
builder.Services.TryAddScoped<ITenantWebRepository, TenantWebRepository>();
builder.Services.TryAddScoped<IUserRepository, UserRepository>();
builder.Services.TryAddScoped<IHeaderRepository, HeaderRepository>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.TryAddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("UteamUP.ServerAPI"));
//builder.Services.AddScoped<IHttpService, HttpService>();

// Add Local Session Storage
builder.Services.AddBlazoredLocalStorage(config => 
{
    config.JsonSerializerOptions.WriteIndented = true;
});

// Add Blazor Toast for Error Messages
builder.Services.AddBlazoredToast();

// Add Fluxor Services
/*
builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(currentAssembly)
        .UseRouting()
        .UseReduxDevTools();
    options.AddMiddleware<LocalStorageMiddleware>();
});
*/
// Configure MSAL authentication
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add(builder.Configuration.GetSection("ServerApi")["Scopes"]);
});


// Get API URL from appsettings.json if empty else use https://dev.uteamup.com/
/*
var apiUrl = builder.Configuration["ApiUrl"] ?? "https://dev.uteamup.com/";
builder.Services.AddHttpClient("Api", httpClient =>
{
    httpClient.BaseAddress = new Uri(apiUrl);
});
*/

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