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
    
    public async Task<Tenant?> CreateTenantAsync(TenantDto tenant, int planId, int extraLicenses)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PostAsJsonAsync($"{Url}/add/{planId}/{extraLicenses}", tenant);
        
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
        try{
            var result = await _httpClient.GetAsync($"{Url}/owned/{oid}");
            
            if (result.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Information, $"{nameof(GetOwnedTenantsAsync)}: Showing all owned tenants");
                return await result.Content.ReadFromJsonAsync<List<Tenant>>();
            }
            else
            {
                _logger.Log(LogLevel.Information,
                    $"{nameof(GetOwnedTenantsAsync)}: User did not have any owned tenants");
                return new List<Tenant>();
            }
        }
        catch(Exception e)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetOwnedTenantsAsync)}: {e.Message}");
            return new List<Tenant>();
        }

    }

    public async Task<List<Tenant>> GetAllTenantsByOidAsync(string oid)
    {
        await GetHttpClientHeaderToken();
        Console.WriteLine($"{Url}/oid/{oid}");
        if (oid == null || oid == "0")
            return new List<Tenant>();

        var result = await _httpClient.GetFromJsonAsync<List<Tenant>>($"{Url}/oid/{oid}");
        return result;
    }

    public async Task<Tenant> GetTenantById(string tenantId)
    {
        await GetHttpClientHeaderToken();
        Console.WriteLine($"{Url}/{tenantId}");
        if(tenantId == "0" || tenantId == null)
            return new Tenant();
        
        var result = await _httpClient.GetFromJsonAsync<Tenant>($"{Url}/{tenantId}");
        return result;
    }

    public async Task<List<Tenant>> GetInvitesAsync(string oid)
    {
        try{
            await GetHttpClientHeaderToken();
            var result = await _httpClient.GetAsync($"{Url}/invites/{oid}");
            if (result.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Information, $"{nameof(GetInvitesAsync)}: Showing all tenant invites");
                return await result.Content.ReadFromJsonAsync<List<Tenant>>();
            }
            else
            {
                _logger.Log(LogLevel.Information,
                    $"{nameof(CreateTenantAsync)}: User did not have any invites");
                return new List<Tenant>();
            }
        }
        catch(Exception e)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetInvitesAsync)}: {e.Message}");
            return new List<Tenant>();
        }
    }
}