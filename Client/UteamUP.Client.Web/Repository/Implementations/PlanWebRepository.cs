using System.Net.Http.Json;
using UteamUP.Client.Web.Repository.Interfaces;

namespace UteamUP.Client.Web.Repository.Implementations;

public class PlanWebRepository : IPlanWebRepository
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private protected string ServerUrl = "https://localhost:5001";
    private protected string Url = "api/plan";
    private protected string UserStateUrl = "api/userstate";

    public PlanWebRepository(
        HttpClient httpClient, 
        AuthenticationStateProvider authenticationStateProvider, 
        IHeaderRepository headerRepository
    )
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
    
    // Create the plan
    public async Task<bool> CreatePlanAsync(PlanDto? plan)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PostAsJsonAsync<PlanDto>($"{ServerUrl}/{Url}", plan);
        if (result.IsSuccessStatusCode)
        {
            await result.Content.ReadFromJsonAsync<PlanDto>();
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<List<Plan?>?> GetAllPlansAsync()
    {
        var result = await _httpClient.GetAsync($"{ServerUrl}/{Url}");
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<List<Plan?>>();
        }
        else
        {
            return new List<Plan?>();
        }
    }
}