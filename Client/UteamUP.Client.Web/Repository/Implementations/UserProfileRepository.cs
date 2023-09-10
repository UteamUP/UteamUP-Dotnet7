using System.Net.Http.Json;
using UteamUP.Shared.States;

namespace UteamUP.Client.Web.Repository.Implementations;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private readonly ILogger<UserProfileRepository> _logger;
    private protected string Url = "api/userprofile";
    
    public UserProfileRepository(
        HttpClient httpClient, 
        AuthenticationStateProvider authenticationStateProvider, 
        IHeaderRepository headerRepository, ILogger<UserProfileRepository> logger)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _headerRepository = headerRepository;
        _logger = logger;
    }
    
    private async Task GetHttpClientHeaderToken()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        _httpClient.DefaultRequestHeaders.Authorization = await _headerRepository.GetHeaderAsync();
    }
    public async Task<GlobalState?> GetUserProfile()
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetFromJsonAsync<GlobalState>($"{Url}");
        
        if (result != null)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetUserProfile)}: User profile retrieved successfully");
            return result;
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(GetUserProfile)}: User profile retrieval failed, because of : " + result);
            return null;
        }
    }
}