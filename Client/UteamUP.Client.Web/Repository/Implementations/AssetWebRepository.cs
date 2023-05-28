using System.Net.Http.Json;
using UteamUP.Client.Web.Repository.Interfaces;

namespace UteamUP.Client.Web.Repository.Implementations;

public class AssetWebRepository : IAssetWebRepository
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private readonly ILogger<PlanWebRepository> _logger;
    private protected string Url = "api/asset";
    
    public AssetWebRepository(
        HttpClient httpClient, 
        AuthenticationStateProvider authenticationStateProvider, 
        IHeaderRepository headerRepository, 
        ILogger<PlanWebRepository> logger
        )
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
    
    public async Task<Asset> CreateAssetAsync(AssetDto? asset, int tenantId = 0, int vendorId = 0)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PostAsJsonAsync<AssetDto>($"{Url}/{tenantId}/{vendorId}", asset);
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(CreateAssetAsync)}: Asset created successfully");
            return await result.Content.ReadFromJsonAsync<Asset>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(CreateAssetAsync)}: Asset creation failed, because of : " + result.StatusCode);
            return null;
        }

    }
}