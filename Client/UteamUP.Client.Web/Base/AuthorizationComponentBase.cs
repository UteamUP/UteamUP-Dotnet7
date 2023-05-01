using Blazored.LocalStorage;
using Newtonsoft.Json;

namespace UteamUP.Client.Web.Base;

public class AuthorizationComponentBase
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<AuthorizationComponentBase> _logger;
    private readonly ILocalStorageService _localStorageService;

    public AuthorizationComponentBase(
        AuthenticationStateProvider authenticationStateProvider, 
        IDispatcher dispatcher,
        ILogger<AuthorizationComponentBase> logger, 
        ILocalStorageService localStorageService
        )
    {
        _authenticationStateProvider = authenticationStateProvider;
        _dispatcher = dispatcher;
        _logger = logger;
        _localStorageService = localStorageService;
    }

    public async Task<bool> SetUserDetails()
    {
        // Get the current user
        _logger.Log(LogLevel.Information, "Getting the current user");
        var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        if (authenticationState.User.Identity == null || !authenticationState.User.Identity.IsAuthenticated)
            return false;
        
        _logger.Log(LogLevel.Information, "The user is authenticated");
        
        // Get the user details and set it to the Global state
        var user = authenticationState.User.FindFirst("name")?.Value;
        var oid = authenticationState.User.FindFirst("oid")?.Value;
        var email = authenticationState.User.FindFirst("signInNames.emailAddress")?.Value;

        // Replace enter with ;, Replace space with ;, Replace tab with ;, Replace comma with ;. If there is only one email, do not add ;
        email = email?.Replace("\r", ";").Replace("\t", ";").Replace(" ", ";").Replace(",", ";");
        if (email?.Contains(";") == true) email = email.Substring(0, email.IndexOf(";"));

        _logger.Log(LogLevel.Information, $"Found : {user} / {oid} / {email}");

        // Check if the persistant storage contains the user details, if not then add it.
        var savedStateJson = await _localStorageService.GetItemAsStringAsync("globalState");
        if (string.IsNullOrWhiteSpace(savedStateJson))
        {
            // Create a new GlobalState
            _logger.Log(LogLevel.Information, $"Creating a new GlobalState");
            var globalState = new GlobalState
            {
                Oid = oid,
                Name = user,
                Email = email
            };
            
            // Save the GlobalState to the local storage
            _logger.Log(LogLevel.Information, $"Saving the GlobalState to the local storage");
            await _localStorageService.SetItemAsync("globalState", globalState);
        }
        else
        {
            // Get the GlobalState from the local storage
            _logger.Log(LogLevel.Information, $"Getting the GlobalState from the local storage");
            var savedState = JsonConvert.DeserializeObject<GlobalState>(savedStateJson);
            
            // Dispatch the GlobalState to the Global state
            _dispatcher.Dispatch(new SetGlobalStateAction(savedState));
        }

        try
        {
            // Set the user details to the Global state
            _logger.Log(LogLevel.Information, $"Saving the user details to the Global state");
            _dispatcher.Dispatch(new SetUserAction(oid, user, email));
            return true;
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return false;
        }
    }
}