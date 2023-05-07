using System.Net.Http.Json;
using UteamUP.Client.Web.Repository.Interfaces;

namespace UteamUP.Client.Web.Repository.Implementations;

public class PlanWebRepository : IPlanWebRepository
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IHeaderRepository _headerRepository;
    private readonly ILogger<PlanWebRepository> _logger;
    private protected string ServerUrl = "https://localhost:5001";
    private protected string Url = "api/plan";

    public PlanWebRepository(
        HttpClient httpClient, 
        AuthenticationStateProvider authenticationStateProvider, 
        IHeaderRepository headerRepository, ILogger<PlanWebRepository> logger)
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
    
    // Create the plan
    public async Task<bool> CreatePlanAsync(PlanDto? plan)
    {
        await GetHttpClientHeaderToken();
        var result = await _httpClient.PostAsJsonAsync<PlanDto>($"{ServerUrl}/{Url}", plan);
        if (result.IsSuccessStatusCode)
        {
            await result.Content.ReadFromJsonAsync<PlanDto>();
            _logger.Log(LogLevel.Information, "Successfully created plan: {plan}", plan.Name);
            return true;
        }
        else
        {
            _logger.Log(LogLevel.Error, "Error creating plan, since the result was not successful: {result}", result.RequestMessage);
            return false;
        }
    }

    public async Task<List<Plan?>?> GetAllPlansAsync()
    {
        var result = await _httpClient.GetAsync($"{ServerUrl}/{Url}");
        if (result.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Information, "Successfully got all plans");
            return await result.Content.ReadFromJsonAsync<List<Plan?>>();
        }
        else
        {
            _logger.Log(LogLevel.Error, "Error getting all plans, since the result was not successful: {result}", result.RequestMessage);
            return new List<Plan?>();
        }
    }
}