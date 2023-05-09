using System.Net.Http.Json;
using UteamUP.Client.Web.Repository.Interfaces;

namespace UteamUP.Client.Web.Repository.Implementations;

public class VendorWebRepository : IVendorWebRepository
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private readonly ILogger<VendorWebRepository> _logger;
    private protected string ServerUrl = "https://localhost:5001";
    private protected string Url = "api/vendor";
    
    public VendorWebRepository(
        HttpClient httpClient, 
        AuthenticationStateProvider authenticationStateProvider, 
        IHeaderRepository headerRepository, 
        ILogger<VendorWebRepository> logger
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
    
    public async Task<bool> CreateAsync(VendorDto vendor)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PostAsJsonAsync<VendorDto>($"{Url}", vendor);
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(CreateAsync)}: Vendor created successfully");
            await result.Content.ReadFromJsonAsync<Vendor>();
            return true;
        }
        else
        {
            _logger.Log(LogLevel.Error,
                $"{nameof(CreateAsync)}: Vendor creation failed, because of : " + result.StatusCode);
            return false;
        }
    }

    public async Task<List<Vendor>> GetAllAsync()
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.GetFromJsonAsync<List<Vendor>>($"{Url}");
        if (result != null)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetAllAsync)}: Vendor retrieved successfully");
            return result;
        }
        else
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetAllAsync)}: Vendor retrieval failed");
            return new List<Vendor>();
        }
    }
}