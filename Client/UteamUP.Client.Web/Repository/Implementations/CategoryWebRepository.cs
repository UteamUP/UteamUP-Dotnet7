using System.Net.Http.Json;
using UteamUP.Client.Web.Repository.Interfaces;

namespace UteamUP.Client.Web.Repository.Implementations;

public class CategoryWebRepository : ICategoryWebRepository
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private readonly ILogger<CategoryWebRepository> _logger;
    private protected string Url = "api/category";

    public CategoryWebRepository(
        HttpClient httpClient, 
        AuthenticationStateProvider authenticationStateProvider, 
        IHeaderRepository headerRepository, ILogger<CategoryWebRepository> logger)
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
    
    // Create the category
    public async Task<bool> CreateAsync(List<CategoryDto> categories, int id)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PostAsJsonAsync($"{Url}/{id}", categories);
        if (result.IsSuccessStatusCode)
        {
            await result.Content.ReadFromJsonAsync<List<CategoryDto>>();
            _logger.Log(LogLevel.Information, $"{nameof(CreateAsync)}: Category created successfully");
            return true;
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(CreateAsync)}: Category creation failed, because of : " + result.StatusCode);
            return false;
        }
    }

    public async Task<List<Category>> GetAllCategoriesByTenantIdAsync(int tenantId)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetFromJsonAsync<List<Category>>($"{Url}/tenant/{tenantId}");
        if (result != null)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetAllCategoriesByTenantIdAsync)}: Categories retrieved successfully");
            return result;
        }
        else
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetAllCategoriesByTenantIdAsync)}: Categories retrieval failed");
            return null;
        }
    }
}