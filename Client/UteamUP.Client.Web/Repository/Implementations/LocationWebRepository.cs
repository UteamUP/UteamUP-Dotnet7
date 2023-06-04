using System.Net;
using System.Net.Http.Json;
using UteamUP.Client.Web.Repository.Interfaces;

namespace UteamUP.Client.Web.Repository.Implementations;

public class LocationWebRepository : ILocationWebRepository
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private readonly ILogger<LocationWebRepository> _logger;
    private protected string Url = "api/location";
    
    public LocationWebRepository(
        HttpClient httpClient, 
        AuthenticationStateProvider authenticationStateProvider, 
        IHeaderRepository headerRepository, 
        ILogger<LocationWebRepository> logger
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
    
    /*
    public async Task<List<Location>> GetAllLocationsByTenantIdAsync(int tenantId)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetAsync($"{Url}/{tenantId}");
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetAllLocationsByTenantIdAsync)}: Locations retrieved successfully");
            return await result.Content.ReadFromJsonAsync<List<Location>>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(GetAllLocationsByTenantIdAsync)}: Locations retrieval failed, because of : " + result.StatusCode);
            return new List<Location>();
        }
    }
    */
    /*
    public async Task<Location> GetByLocationId(int locationId)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetAsync($"{Url}/{locationId}");
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetByLocationId)}: Location retrieved successfully");
            return await result.Content.ReadFromJsonAsync<Location>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(GetByLocationId)}: Location retrieval failed, because of : " + result.StatusCode);
            return null;
        }
    }
    */
    public async Task<Location?> Create(LocationTagDto location)
    {
        await GetHttpClientHeaderToken();
        
        var result = await _httpClient.PostAsJsonAsync<LocationTagDto>("api/location/add", location);
        return await result.Content.ReadFromJsonAsync<Location>();
        /*if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Create)}: Location created successfully");
            return await result.Content.ReadFromJsonAsync<Location>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(Create)}: Location creation failed, because of : " + result.StatusCode + "\n" + result.ReasonPhrase + "\n" + result.Content.ReadAsStringAsync().Result);
            return result.StatusCode == HttpStatusCode.BadRequest ? null : new Location();
        }*/
    }
    
    /*
    public async Task<Location?> Update(Location location)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PutAsJsonAsync<Location>("api/location", location);
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Update)}: Location updated successfully");
            return await result.Content.ReadFromJsonAsync<Location>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(Update)}: Location update failed, because of : " + result.StatusCode);
            return null;
        }
    }
    */
    /*
    public async Task<List<Tag>> GetTagsByLocationId(int locationId)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetAsync($"{Url}/{locationId}/tags");
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetTagsByLocationId)}: Tags retrieved successfully");
            return await result.Content.ReadFromJsonAsync<List<Tag>>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(GetTagsByLocationId)}: Tags retrieval failed, because of : " + result.StatusCode);
            return new List<Tag>();
        }
    }
    */
    /*
    public async Task<Location?> UpdateTagToLocationAsync(List<Tag> tags, int locationId)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PutAsJsonAsync<List<Tag>>($"{Url}/{locationId}/tags", tags);
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(UpdateTagToLocationAsync)}: Tags updated successfully");
            return await result.Content.ReadFromJsonAsync<Location>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(UpdateTagToLocationAsync)}: Tags update failed, because of : " + result.StatusCode);
            return null;
        }
    }
    */
}