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
        Console.WriteLine($"CreateTenantAsync: Creating tenant ({Url})");
        var result = await _httpClient.PostAsJsonAsync($"{Url}", tenant);
        
        /*if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, "CreateTenantAsync: Tenant created successfully");
            return await result.Content.ReadFromJsonAsync<Tenant>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(CreateTenantAsync)}: Tenant creation failed, because of : " + result.StatusCode);
            return new Tenant();
        }*/
        return new Tenant();
    }
}