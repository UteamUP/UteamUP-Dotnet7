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
    public async Task<Tag> CreateAsync(TagDto tag)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PostAsJsonAsync($"{Url}", tag);
        if (result.IsSuccessStatusCode)
        {
            var myTag = await result.Content.ReadFromJsonAsync<Tag>();
            _logger.Log(LogLevel.Information, $"{nameof(CreateManyAsync)}: Tag created successfully");
            return myTag;
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(CreateManyAsync)}: Tag creation failed, because of : " + result.StatusCode);
            return new Tag();
        }
    }
    
    // Create the tag
    public async Task<bool> CreateManyAsync(List<TagDto> tags)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PostAsJsonAsync($"{Url}/many", tags);
        if (result.IsSuccessStatusCode)
        {
            await result.Content.ReadFromJsonAsync<List<TagDto>>();
            _logger.Log(LogLevel.Information, $"{nameof(CreateManyAsync)}: Tag created successfully");
            return true;
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(CreateManyAsync)}: Tag creation failed, because of : " + result.StatusCode);
            return false;
        }
    }

    public async Task<Tag> GetTagByNameAsync(string name)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetAsync($"{Url}/{name}");
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetTagByNameAsync)}: Tag found successfully");
            return await result.Content.ReadFromJsonAsync<Tag>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(GetTagByNameAsync)}: Tag not found, because of : " + result.StatusCode);
            return null;
        }
    }

    public async Task<Tag> GetTagByNameAndLocationNameAsync(string name, string locationName)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetAsync($"{Url}/{name}/{locationName}");
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetTagByNameAndLocationNameAsync)}: Tag found successfully");
            return await result.Content.ReadFromJsonAsync<Tag>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(GetTagByNameAndLocationNameAsync)}: Tag not found, because of : " + result.StatusCode);
            return null;
        }
    }

    public async Task<Tag> GetTagByNameAndTenantIdAsync(string tagName, int tenantId)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetAsync($"{Url}/{tagName}/tenant/{tenantId}");
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetTagByNameAndTenantIdAsync)}: Tag found successfully");
            return await result.Content.ReadFromJsonAsync<Tag>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(GetTagByNameAndTenantIdAsync)}: Tag not found, because of : " + result.StatusCode);
            return null;
        }
    }

    public async Task<Tag> GetOrCreateTagAsync(string name, int tenantId)
    {
        TagDto tagDto = new TagDto()
        {
            Name = name,
            TenantId = tenantId
        };
        
        var tag = await GetTagByNameAndTenantIdAsync(name, tenantId);
        if(string.IsNullOrWhiteSpace(tag.Name))
            return await CreateAsync(tagDto);

        return tag;
    }
}