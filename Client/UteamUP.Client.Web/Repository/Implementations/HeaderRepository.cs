using System.Net.Http.Headers;
using UteamUP.Client.Web.Repository.Interfaces;

namespace UteamUP.Client.Web.Repository.Implementations;

public class HeaderRepository : IHeaderRepository
{
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly ILogger<HeaderRepository> _logger;
    
    public HeaderRepository(IAccessTokenProvider accessTokenProvider, ILogger<HeaderRepository> logger)
    {
        _accessTokenProvider = accessTokenProvider;
        _logger = logger;
    }
    
    public async Task<AuthenticationHeaderValue> GetHeaderAsync()
    {
        AccessToken token;
        
        var tokenResult = await _accessTokenProvider.RequestAccessToken();

        if (tokenResult.TryGetToken(out token))
        {
            _logger.Log(LogLevel.Information, $"SetHeaderAsync: {token.Value}");
            return new AuthenticationHeaderValue("Bearer", token.Value);
        }
        
        _logger.Log(LogLevel.Error, $"SetHeaderAsync: Unable to obtain an access token.");
        throw new InvalidOperationException("Unable to obtain an access token.");
    }
}