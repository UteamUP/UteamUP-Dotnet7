using System.Net.Http.Json;
using Newtonsoft.Json;

namespace UteamUP.Client.Web.Repository.Implementations;

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
    
    public async Task<Tenant?> CreateTenantAsync(Tenant tenant, int planId, int extraLicenses)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PostAsJsonAsync($"{Url}/add/plan/{planId}/extraLicenses/{extraLicenses}", tenant);
        
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

    public async Task<Tenant?> UpdateTenantAsync(Tenant tenant, int planId, int extraLicenses)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PutAsJsonAsync($"{Url}/plan/{planId}/extraLicenses/{extraLicenses}", tenant);
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(UpdateTenantAsync)}: Tenant updated successfully");
            return await result.Content.ReadFromJsonAsync<Tenant>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(UpdateTenantAsync)}: Tenant update failed, because of : " + result.StatusCode);
            return new Tenant();
        }
    }

    public async Task<List<Tenant>> GetOwnedTenantsAsync(string oid)
    {
        await GetHttpClientHeaderToken();

        var result = await _httpClient.GetAsync($"{Url}/owned/{oid}").ConfigureAwait(false);

        result.EnsureSuccessStatusCode();

        var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
        var output = JsonConvert.DeserializeObject<List<Tenant>>(content);
        
        return output;
    }

    public async Task<List<Tenant>> GetAllTenantsByOidAsync(string oid)
    {
        await GetHttpClientHeaderToken();
        Console.WriteLine($"{Url}/oid/{oid}");
        if (oid == null || oid == "0")
            return new List<Tenant>();

        var result = await _httpClient.GetAsync($"{Url}/oid/{oid}").ConfigureAwait(false);
        
        result.EnsureSuccessStatusCode();

        var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
        var output = JsonConvert.DeserializeObject<List<Tenant>>(content);
        
        return output;
    }

    public async Task<Tenant> GetTenantById(int tenantId)
    {
        await GetHttpClientHeaderToken();

        var result = await _httpClient.GetAsync($"{Url}/{tenantId}").ConfigureAwait(false);

        result.EnsureSuccessStatusCode();

        var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
        var output = JsonConvert.DeserializeObject<Tenant>(content);
        
        return output;
    }

    public async Task<List<Tenant>> GetInvitesAsync(string oid)
    {
        await GetHttpClientHeaderToken();

        var result = await _httpClient.GetAsync($"{Url}/invites/{oid}").ConfigureAwait(false);

        result.EnsureSuccessStatusCode();

        var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
        var output = JsonConvert.DeserializeObject<List<Tenant>>(content);
        
        return output;
    }
    

}