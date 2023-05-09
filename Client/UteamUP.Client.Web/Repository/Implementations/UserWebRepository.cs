using System.Net.Http.Headers;
using System.Net.Http.Json;
using UteamUP.Client.Web.Repository.Interfaces;

namespace UteamUP.Client.Web.Repository.Implementations;

public class UserWebRepository : IUserWebRepository
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private readonly ILogger<UserWebRepository> _logger;
    private protected string ServerUrl = "https://localhost:5001";
    private protected string Url = "api/user/oid";
    private protected string UserStateUrl = "api/userstate";

    public UserWebRepository(
        HttpClient httpClient, 
        AuthenticationStateProvider authenticationStateProvider, 
        IHeaderRepository headerRepository, 
        ILogger<UserWebRepository> logger
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
    
    public async Task<MUser?> GetUserByOid(string? oid)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        _httpClient.DefaultRequestHeaders.Authorization = await _headerRepository.GetHeaderAsync();

        return await _httpClient.GetFromJsonAsync<MUser>($"{Url}/{oid}");
    }

    public async Task<bool> CreateUserAsync(MUserDto user)
    {
        await GetHttpClientHeaderToken();
        var newUser = await _httpClient.PostAsJsonAsync($"{Url}", user);
        if (newUser.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(CreateUserAsync)}: User created successfully");
            return true;
        }
        else
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateUserAsync)}: User creation failed with status code {newUser.StatusCode} and reason {newUser.ReasonPhrase}");
            return false;
        }
    }

    public async Task<MUserUpdateDto?> UpdateUserByOid(MUserUpdateDto? userUpdateDto, string oid)
    {
        await GetHttpClientHeaderToken();
        var user = await _httpClient.PutAsJsonAsync<MUserUpdateDto>($"{Url}/{oid}", userUpdateDto);
        
        if (user.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(UpdateUserByOid)}: User updated successfully");
            return await user.Content.ReadFromJsonAsync<MUserUpdateDto>();
        }
        else
        {
            _logger.Log(LogLevel.Error, $"{nameof(UpdateUserByOid)}: User update failed with status code {user.StatusCode} and reason {user.ReasonPhrase}");
            return new MUserUpdateDto();
        }
    }

    public async Task<bool> ActivateUser(string activationCode, string oid)
    {
        await GetHttpClientHeaderToken();
        var user = await _httpClient.PostAsync($"{Url}/{oid}/activate/{activationCode}", null);
        if (user.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, $"{nameof(ActivateUser)}: User activated successfully");
            return true;
        }
        else
        {
            _logger.Log(LogLevel.Error, $"{nameof(ActivateUser)}: User activation failed with status code {user.StatusCode} and reason {user.ReasonPhrase}");
            return false;
        }

    }
}