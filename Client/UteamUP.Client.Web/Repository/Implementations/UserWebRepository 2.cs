using System.Net.Http.Headers;
using System.Net.Http.Json;
using UteamUP.Client.Web.Repository.Interfaces;

namespace UteamUP.Client.Web.Repository.Implementations;

public class UserWebRepository : IUserWebRepository
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private protected string ServerUrl = "https://localhost:5001";
    private protected string Url = "api/user/oid";
    private protected string UserStateUrl = "api/userstate";

    public UserWebRepository(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IHeaderRepository headerRepository)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _headerRepository = headerRepository;
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
        await GetHttpClientHeaderToken();

        var muser = await _httpClient.GetFromJsonAsync<MUser>($"{Url}/{oid}");
        if (string.IsNullOrWhiteSpace(muser.Oid))
            return new MUser();
        
        return muser;
    }

    public async Task<MUserUpdateDto?> UpdateUserByOid(MUserUpdateDto? userUpdateDto, string oid)
    {
        await GetHttpClientHeaderToken();
        var user = await _httpClient.PutAsJsonAsync<MUserUpdateDto>($"{ServerUrl}/{Url}/{oid}", userUpdateDto);
        
        if (user.IsSuccessStatusCode)
        {
            return await user.Content.ReadFromJsonAsync<MUserUpdateDto>();
        }
        else
        {
            return new MUserUpdateDto();
        }
    }
}