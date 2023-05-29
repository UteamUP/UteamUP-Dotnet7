using System.Net.Http.Json;
using UteamUP.Client.GlobalRepository.Interfaces;
using UteamUP.Client.Web.Repository.Interfaces;

namespace UteamUP.Client.GlobalRepository.Implementations;

public class WebRepository<T> : IWebRepository<T> where T : class
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private readonly ILogger<T> _logger;

    public WebRepository(
        HttpClient httpClient,
        AuthenticationStateProvider authenticationStateProvider,
        IHeaderRepository headerRepository,
        ILogger<T> logger
        )
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
    
    public async Task<IEnumerable<T>> GetAll(string url)
    {
        url = url.ToLower();
        
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetFromJsonAsync<IEnumerable<T>>(url);
        
        if (result != null)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetAll)}: {typeof(T)} retrieved successfully");
            return result;
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(GetAll)}: {typeof(T)} retrieval failed");
            return new List<T>();
        }

    }

    public async Task<IEnumerable<T>> GetAllByTenantId(string url, int tenantId)
    {
        url = url.ToLower();
        
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetFromJsonAsync<IEnumerable<T>>($"{url}/tenant/{tenantId}");
        
        if (result != null)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetAllByTenantId)}: {typeof(T)} retrieved successfully");
            return result;
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(GetAllByTenantId)}: {typeof(T)} retrieval failed");
            return new List<T>();
        }
    }

    public async Task<T> Get(int id, string url)
    {
        url = url.ToLower();

        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetFromJsonAsync<T>($"{url}/{id}");
        
        if (result != null)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Get)}: {typeof(T)} retrieved successfully");
            return result;
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(Get)}: {typeof(T)} retrieval failed");
            return Activator.CreateInstance<T>();
        }

    }

    public async Task<T> GetByName(string name, string url)
    {
        url = url.ToLower();

        await GetHttpClientHeaderToken();
        
        var result = await _httpClient.GetFromJsonAsync<T>($"{url}/name/{name}");
        
        if (result != null)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetByName)}: {typeof(T)} retrieved successfully");
            return result;
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(GetByName)}: {typeof(T)} retrieval failed");
            return Activator.CreateInstance<T>();
        }
    }

    public async Task<T> GetByNameAndTenantId(string name, string url, int tenantId)
    {
        url = url.ToLower();
        
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetFromJsonAsync<T>($"{url}/name/{name}/tenant/{tenantId}");
        
        if (result != null)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetByNameAndTenantId)}: {typeof(T)} retrieved successfully");
            return result;
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(GetByNameAndTenantId)}: {typeof(T)} retrieval failed");
            return Activator.CreateInstance<T>();
        }
    }

    public async Task<T> Add(T entity, string url)
    {
        url = url.ToLower();

        await GetHttpClientHeaderToken();
        var result = await _httpClient.PostAsJsonAsync<T>(url, entity);
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Add)}: {typeof(T)} created successfully");
            return await result.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(Add)}: {typeof(T)} creation failed, because of : " + result.StatusCode);
            return Activator.CreateInstance<T>();
        }
    }

    public async Task<T> AddByTenantId(T entity, string url, int tenantId)
    {
        url = url.ToLower();
        
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PostAsJsonAsync<T>($"{url}/tenant/{tenantId}", entity);
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(AddByTenantId)}: {typeof(T)} created successfully");
            return await result.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(AddByTenantId)}: {typeof(T)} creation failed, because of : " + result.StatusCode);
            return Activator.CreateInstance<T>();
        }
    }

    public async Task<T> Update(T entity, string url)
    {
        url = url.ToLower();

        await GetHttpClientHeaderToken();
        var result = await _httpClient.PutAsJsonAsync<T>(url, entity);
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Update)}: {typeof(T)} updated successfully");
            return await result.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(Update)}: {typeof(T)} update failed, because of : " + result.StatusCode);
            return Activator.CreateInstance<T>();
        }
    }

    public async Task<T> UpdateByTenantId(T entity, string url, int tenantId)
    {
        url = url.ToLower();
        
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PutAsJsonAsync<T>($"{url}/tenant/{tenantId}", entity);
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(UpdateByTenantId)}: {typeof(T)} updated successfully");
            return await result.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(UpdateByTenantId)}: {typeof(T)} update failed, because of : " + result.StatusCode);
            return Activator.CreateInstance<T>();
        }
    }

    public async Task<T> Delete(T entity, string url)
    {
        url = url.ToLower();

        await GetHttpClientHeaderToken();
        var result = await _httpClient.DeleteAsync($"{url}/{entity}");
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(Delete)}: {typeof(T)} deleted successfully");
            return await result.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(Delete)}: {typeof(T)} deletion failed, because of : " + result.StatusCode);
            return Activator.CreateInstance<T>();
        }
    }

    public async Task<T> DeleteByTenantId(T entity, string url, int tenantId)
    {
        url = url.ToLower();
        
        await GetHttpClientHeaderToken();
        var result = await _httpClient.DeleteAsync($"{url}/tenant/{tenantId}/{entity}");
        
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(DeleteByTenantId)}: {typeof(T)} deleted successfully");
            return await result.Content.ReadFromJsonAsync<T>();
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(DeleteByTenantId)}: {typeof(T)} deletion failed, because of : " + result.StatusCode);
            return Activator.CreateInstance<T>();
        }
    }
}