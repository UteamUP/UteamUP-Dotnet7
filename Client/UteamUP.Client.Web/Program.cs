using Blazor.SubtleCrypto;
using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor.Services;
using UteamUP.Client.GlobalRepository.Implementations;
using UteamUP.Client.GlobalRepository.Interfaces;
using UteamUP.Client.Web.Repository.Implementations;
using UteamUP.Client.Web.Repository.Interfaces;
using UteamUP.Client.Web.Services;
using UteamUP.Client.Web.Shared;
using UteamUP.Shared.States;
using Fluxor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var currentAssembly = typeof(Program).Assembly;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add Fluxor
builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(currentAssembly);
});

// Add Base Component Services
builder.Services.AddScoped<UserState>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
//builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthenticationStateProvider>());

// Adding generic repository services.
builder.Services.AddScoped(typeof(IWebRepository<>), typeof(WebRepository<>));
builder.Services.AddScoped<Radzen.ContextMenuService>();

// Add Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Add Repositories
builder.Services.AddScoped<ITenantWebRepository, TenantWebRepository>();
builder.Services.AddScoped<IUserWebRepository, UserWebRepository>();
builder.Services.AddScoped<IPlanWebRepository, PlanWebRepository>();
builder.Services.AddScoped<IHeaderRepository, HeaderRepository>();
builder.Services.AddScoped<IWorkorderWebRepository, WorkorderWebRepository>();
builder.Services.AddScoped<IVendorWebRepository, VendorWebRepository>();
builder.Services.AddScoped<ICategoryWebRepository, CategoryWebRepository>();
builder.Services.AddScoped<ITagWebRepository, TagWebRepository>();
builder.Services.AddScoped<IDocumentWebRepository, DocumentWebRepository>();
builder.Services.AddScoped<IPartWebRepository, PartWebRepository>();
builder.Services.AddScoped<IAssetWebRepository, AssetWebRepository>();
builder.Services.AddScoped<ILocationWebRepository, LocationWebRepository>();
builder.Services.AddScoped<ISubscriptionWebRepository, SubscriptionWebRepository>();

// Add Services
builder.Services.AddScoped<IBlobStorageWebService, BlobStorageWebService>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("UteamUP.ServerAPI"));

// Add Local Session Storage
builder.Services.AddBlazoredLocalStorage(config => 
{
    config.JsonSerializerOptions.WriteIndented = true;
});

// Add Blazor Modal for Modals
builder.Services.AddBlazoredModal();

// Add MudBlazor Services
builder.Services.AddMudServices();


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

builder.Services.AddLogging();

builder.Services.AddMemoryCache();


builder.Services.AddHttpClient("UteamUP.ServerAPI",
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler();
        handler.MaxRequestContentBufferSize = 50 * 1024 * 1024; // 50 MB
        return handler;
    });
    

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