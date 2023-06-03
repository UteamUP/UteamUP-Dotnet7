using System.Net.Http.Json;

namespace UteamUP.Client.Web.Repository.Implementations;

public class SubscriptionWebRepository : ISubscriptionWebRepository
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private readonly ILogger<SubscriptionWebRepository> _logger;
    private protected string Url = "api/subscription";
    
    public SubscriptionWebRepository(
        HttpClient httpClient, 
        AuthenticationStateProvider authenticationStateProvider, 
        IHeaderRepository headerRepository, ILogger<SubscriptionWebRepository> logger)
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


    public async Task<Subscription> GetSubscriptionByTenantId(int tenantId)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetAsync($"{Url}/tenant/{tenantId}");
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetSubscriptionByTenantId)}: Subscription retrieved successfully");
            return await result.Content.ReadFromJsonAsync<Subscription>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(GetSubscriptionByTenantId)}: Subscription retrieval failed, because of : " + result.StatusCode);
            return new Subscription();
        }

    }
}