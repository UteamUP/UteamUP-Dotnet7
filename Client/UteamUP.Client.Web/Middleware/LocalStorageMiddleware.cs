using Blazored.LocalStorage;
using Newtonsoft.Json;
using UteamUP.Client.Middleware;

public class LocalStorageMiddleware : IMiddleware
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<LocalStorageMiddleware> _logger;
    private readonly IAccessTokenProvider _accessTokenProvider;

    public bool _isUserLoggedIn = false;
    
    public LocalStorageMiddleware(
        ILocalStorageService localStorageService, 
        IDispatcher dispatcher, 
        ILogger<LocalStorageMiddleware> logger, 
        IAccessTokenProvider accessTokenProvider
        )
    {
        _localStorageService = localStorageService;
        _dispatcher = dispatcher;
        _logger = logger;
        _accessTokenProvider = accessTokenProvider;
    }
    
    public async Task LoadStateAsync()
    {
        _logger.Log(LogLevel.Information, "Load for the first time the state from the local storage");

        try
        {
            var savedStateJson = await _localStorageService.GetItemAsStringAsync("globalState");
            if (!string.IsNullOrWhiteSpace(savedStateJson))
            {
                var savedState = JsonConvert.DeserializeObject<GlobalState>(savedStateJson);
                if (savedState != null)
                {
                    _logger.Log(LogLevel.Information, "Deserialized the state from the local storage");
                    _dispatcher.Dispatch(new SetGlobalStateAction(savedState));
                }
            }
        }catch(Exception e)
        {
            _logger.Log(LogLevel.Error, $"LoadStateAsync: Got an error: {e.Message}");
        }
    }
    
    public Task InitializeAsync(IDispatcher dispatcher, IStore store)
    {
        _logger.Log(LogLevel.Information, "InitializeAsync: Initializing the middleware");
        return Task.CompletedTask;
    }

    public void AfterInitializeAllMiddlewares()
    {
        //_logger.Log(LogLevel.Information, "AfterInitializeAllMiddlewares: After initializing all middlewares");

        // You can add any code here that should run after all middlewares are initialized
    }

    public bool MayDispatchAction(object action)
    {
        return true;
    }

    public async void BeforeDispatch(object action)
    {
        //_logger.Log(LogLevel.Information, "BeforeDispatch: Before dispatching an action");

        // Load the state only for the first time
        // await LoadStateAsync();
    }

    private async Task<bool> GetGlobalSateAsync()
    {
        //_logger.Log(LogLevel.Information, "GetGlobalSateAsync: Get the state from fluxor.");
        
        return true;
    }
    
    private async Task<bool> GetPersistantSateAsync()
    {
        _logger.Log(LogLevel.Information, "GetPersistantSateAsync: Get the local session storage.");
        var savedStateJson = await _localStorageService.GetItemAsStringAsync("globalState");
        if (!string.IsNullOrWhiteSpace(savedStateJson))
        {
            _logger.Log(LogLevel.Information, "GetPersistantSateAsync: The state was found from the local storage.");
            return true;
        }

        return false;
    }

    
    
    private async Task<bool> IsUserLoggedIn()
    {
        try{
            var tokenResult = await _accessTokenProvider.RequestAccessToken();
            _logger.Log(LogLevel.Information, "IsUserLoggedIn: Trying to get token");

            if (tokenResult.TryGetToken(out var token))
            {
                _logger.Log(LogLevel.Information, "IsUserLoggedIn: Token was found.");
                return true;
            }
            _logger.Log(LogLevel.Information, "IsUserLoggedIn: Token was not found.");
            return false;
        }catch(Exception e)
        {
            _logger.Log(LogLevel.Error, $"IsUserLoggedIn: Got an error: {e.Message}");
            return false;
        }
    }
    
    public async void AfterDispatch(object action)
    {
    }
    
    public IDisposable BeginInternalMiddlewareChange()
    {
        //_logger.Log(LogLevel.Information, "BeginInternalMiddlewareChange: Begin internal middleware change");
        
        return new EmptyDisposable();
    }
}