using System.Net.Http.Headers;
using System.Net.Http.Json;
using UteamUP.Client.Web.Repository.Interfaces;

namespace UteamUP.Client.Web.Repository.Implementations;

public class UserRepository : IUserRepository
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private protected string ServerUrl = "https://localhost:5001";
    private protected string Url = "api/user/oid";
    private protected string UserStateUrl = "api/userstate";

    public UserRepository(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, IHeaderRepository headerRepository)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _headerRepository = headerRepository;
    }

    public async Task<MUser?> GetUserByOid(string? oid)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        _httpClient.DefaultRequestHeaders.Authorization = await _headerRepository.GetHeaderAsync();

        return await _httpClient.GetFromJsonAsync<MUser>($"{ServerUrl}/{Url}/{oid}");
    }
}