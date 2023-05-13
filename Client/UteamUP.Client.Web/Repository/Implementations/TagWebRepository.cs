using System.Net.Http.Json;
using UteamUP.Client.Web.Repository.Interfaces;

namespace UteamUP.Client.Web.Repository.Implementations;

public class TagWebRepository : ITagWebRepository
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private readonly ILogger<TagWebRepository> _logger;
    private protected string Url = "api/tag";
    
    public TagWebRepository(
        HttpClient httpClient, 
        AuthenticationStateProvider authenticationStateProvider, 
        IHeaderRepository headerRepository, ILogger<TagWebRepository> logger)
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
    
    // Create the tag
    public async Task<bool> CreateAsync(List<TagDto> tags)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PostAsJsonAsync($"{Url}", tags);
        if (result.IsSuccessStatusCode)
        {
            await result.Content.ReadFromJsonAsync<List<TagDto>>();
            _logger.Log(LogLevel.Information, $"{nameof(CreateAsync)}: Tag created successfully");
            return true;
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(CreateAsync)}: Tag creation failed, because of : " + result.StatusCode);
            return false;
        }
    }
}