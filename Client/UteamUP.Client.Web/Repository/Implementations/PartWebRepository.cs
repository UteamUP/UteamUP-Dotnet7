using System.Net.Http.Json;
using UteamUP.Client.Web.Repository.Interfaces;

namespace UteamUP.Client.Web.Repository.Implementations;

public class PartWebRepository : IPartWebRepository
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private readonly ILogger<PlanWebRepository> _logger;
    private protected string Url = "api/part";
    
    public PartWebRepository(
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
    
    public async Task<Part> CreatePartAsync(PartDto? part, int tenantId)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PostAsJsonAsync<PartDto>($"{Url}/{tenantId}", part);
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(CreatePartAsync)}: Part created successfully");
            return await result.Content.ReadFromJsonAsync<Part>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(CreatePartAsync)}: Part creation failed, because of : " + result.StatusCode);
            return null;
        }

    }
}