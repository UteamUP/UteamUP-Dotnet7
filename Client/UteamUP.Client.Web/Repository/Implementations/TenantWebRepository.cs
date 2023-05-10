using System.Net.Http.Json;
using UteamUP.Client.Web.Repository.Interfaces;
using UteamUP.Shared.ModelDto;

namespace UteamUP.Client.Repository.Implementations;

public class TenantWebRepository : ITenantWebRepository
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private readonly ILogger<TenantWebRepository> _logger;
    private protected string Url = "api/tenant";
    
    public TenantWebRepository(
        HttpClient httpClient, 
        AuthenticationStateProvider authenticationStateProvider, 
        IHeaderRepository headerRepository, ILogger<TenantWebRepository> logger)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _headerRepository = headerRepository;
        _logger = logger;
    }
    
    // a private function that updates the header with the current user's token
    private async Task GetHttpClientHeaderToken()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        _httpClient.DefaultRequestHeaders.Authorization = await _headerRepository.GetHeaderAsync();
    }
    
    public async Task<Tenant?> CreateTenantAsync(TenantDto tenant)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PostAsJsonAsync($"{Url}", tenant);
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(CreateTenantAsync)}: Tenant created successfully");
            return await result.Content.ReadFromJsonAsync<Tenant>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(CreateTenantAsync)}: Tenant creation failed, because of : " + result.StatusCode);
            return new Tenant();
        }
    }

    public async Task<List<Tenant>> GetOwnedTenantsAsync(string oid)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetAsync($"{Url}/owned/{oid}");
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetOwnedTenantsAsync)}: Showing all owned tenants");
            return await result.Content.ReadFromJsonAsync<List<Tenant>>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(GetOwnedTenantsAsync)}: Showing owned tenants failed: " + result.StatusCode + " and " +
                result.ReasonPhrase);
            return new List<Tenant>();
        }

    }

    public async Task<List<Tenant>> GetMyTenantsAsync(string oid)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetAsync($"{Url}/my/{oid}");
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetMyTenantsAsync)}: Showing all my tenants");
            return await result.Content.ReadFromJsonAsync<List<Tenant>>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(GetMyTenantsAsync)}: Showing my tenants failed: " + result.StatusCode + " and " +
                result.ReasonPhrase);
            return new List<Tenant>();
        }
    }

    public async Task<List<Tenant>> GetInvitesAsync(string oid)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetAsync($"{Url}/invites/{oid}");
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetInvitesAsync)}: Showing all tenant invites");
            return await result.Content.ReadFromJsonAsync<List<Tenant>>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(CreateTenantAsync)}: Could not show any tenant invites: " + result.StatusCode + " and " +
                result.ReasonPhrase);
            return new List<Tenant>();
        }
    }
}