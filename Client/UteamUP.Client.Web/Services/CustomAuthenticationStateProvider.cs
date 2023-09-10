using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UteamUP.Client.Web.Repository.Interfaces;
using UteamUP.Shared.States;
using Blazored.LocalStorage;

namespace UteamUP.Client.Web.Services;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IUserWebRepository _userWebRepository;
    private readonly ITenantWebRepository _tenantWebRepository;
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly UserState _userState;
    private readonly ILogger<CustomAuthenticationStateProvider> _logger;

    public CustomAuthenticationStateProvider(
        ILocalStorageService localStorageService,
        IUserWebRepository userWebRepository,
        IAccessTokenProvider accessTokenProvider,
        UserState userState,
        ILogger<CustomAuthenticationStateProvider> logger,
        ITenantWebRepository tenantWebRepository
    )
    {
        _localStorageService = localStorageService;
        _userWebRepository = userWebRepository;
        _accessTokenProvider = accessTokenProvider;
        _userState = userState;
        _logger = logger;
        _tenantWebRepository = tenantWebRepository;
    }

    // Retrieves the authentication state asynchronously
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        _logger.Log(LogLevel.Information,
            $"{nameof(GetAuthenticationStateAsync)}: Getting authentication claims state");
        var claims = await GetClaimsFromLocalStorageAsync();
        //if(string.IsNullOrWhiteSpace(claims.FirstOrDefault(x => x.Type == "oid").Value)) claims = await GetClaimsFromTokenAsync();
        //if(string.IsNullOrWhiteSpace(claims.FirstOrDefault(x => x.Type == "oid").Value)) return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            
        var identity = new ClaimsIdentity(claims, "Authentication");
        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
        
        /*try
        {
            _logger.Log(LogLevel.Information,
                $"{nameof(GetAuthenticationStateAsync)}: Getting authentication claims state");
            var claims = await GetClaimsFromLocalStorageAsync();
            if(string.IsNullOrWhiteSpace(claims.FirstOrDefault(x => x.Type == "oid").Value)) claims = await GetClaimsFromTokenAsync();
            if(string.IsNullOrWhiteSpace(claims.FirstOrDefault(x => x.Type == "oid").Value)) return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            
            var identity = new ClaimsIdentity(claims, "Authentication");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }
        catch(Exception ex)
        {
            _logger.Log(LogLevel.Error, ex, $"{nameof(GetAuthenticationStateAsync)} failed");
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }*/
    }

    // Retrieves claims from local storage asynchronously
    private async Task<IEnumerable<Claim>> GetClaimsFromLocalStorageAsync()
    {
        try{
            _logger.Log(LogLevel.Information,
                $"{nameof(GetClaimsFromLocalStorageAsync)}: Getting claims from local storage");
            var savedStateJson = await _localStorageService.GetItemAsStringAsync("globalState");
            OnGlobalStateChanged?.Invoke();

            if (string.IsNullOrEmpty(savedStateJson)) return null;

            _logger.Log(LogLevel.Information, $"{nameof(GetClaimsFromLocalStorageAsync)}: Found saved state");
            var globalState = await _localStorageService.GetItemAsync<GlobalState>("globalState");

            _userState.SetUser(CreateMUserFromGlobalState(globalState));

            return new[]
            {
                new Claim("name", globalState.Name),
                new Claim("email", globalState.Email),
                new Claim("oid", globalState.Oid)
            };
        }catch(Exception ex)
        {
            _logger.Log(LogLevel.Error, ex, $"{nameof(GetClaimsFromLocalStorageAsync)} failed");
            // Returns an empty claim
            return new[]
            {
                new Claim("name", ""),
                new Claim("email", ""),
                new Claim("oid", "")
            };
        }
    }

    // Retrieves claims from the token asynchronously
    private async Task<IEnumerable<Claim>> GetClaimsFromTokenAsync()
    {
        var tokenResult = await _accessTokenProvider.RequestAccessToken();

        if (!tokenResult.TryGetToken(out var accessToken)) return null;

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(accessToken.Value);

        var name = jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        var email = jwtToken.Claims.FirstOrDefault(c => c.Type == "signInNames.emailAddress")?.Value;
        var oid = jwtToken.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;

        return string.IsNullOrEmpty(oid)
            ? null
            : new[]
            {
                new Claim("name", name),
                new Claim("email", email),
                new Claim("oid", oid)
            };
    }

    // Creates an MUser from the given global state
    private static MUser CreateMUserFromGlobalState(GlobalState globalState)
    {
        return new MUser
        {
            Name = globalState.Name,
            Email = globalState.Email,
            Oid = globalState.Oid,
            //Tenants = globalState.Tenants,
            DefaultTenantId = globalState.DefaultTenantId,
            HasBeenActivated = globalState.IsActivated,
            IsFirstLogin = globalState.FirstLogin
        };
    }

    // Updates the app state with the user information
    public async Task UpdateAppStateWithUserAsync(ClaimsPrincipal user)
    {
        try{
            _logger.Log(LogLevel.Information,
                $"{nameof(UpdateAppStateWithUserAsync)}: Updating app state with user information");
            var oidClaim = user.Claims.FirstOrDefault(c => c.Type == "oid");
            var nameClaim = user.Claims.FirstOrDefault(c => c.Type == "name");
            var emailClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email) ??
                             user.Claims.FirstOrDefault(c => c.Type == "signInNames.emailAddress") ??
                             user.Claims.FirstOrDefault(c => c.Type == "email");

            Console.WriteLine("I found the OID: " + oidClaim?.Value);
            if (!string.IsNullOrWhiteSpace(oidClaim.Value))
            {
                var oid = oidClaim.Value;
                _logger.Log(LogLevel.Information,
                    $"{nameof(UpdateAppStateWithUserAsync)}: Getting user information from database");
                var muser = await GetUserInformationFromDB(oid);

                // Check if the global state exists
                var globalState = await _localStorageService.GetItemAsync<GlobalState>("globalState");

                // if the global state does not exist, create it and update it with the user information
                if (globalState == null)
                {
                    _logger.Log(LogLevel.Information,
                        $"{nameof(UpdateAppStateWithUserAsync)}: The global state does not exist, creating a new one");
                    var newMUserDto = new MUserDto
                    {
                        Name = nameClaim.Value,
                        Email = emailClaim.Value,
                        Oid = oidClaim.Value
                    };

                    // if the muser is null, create a new user and return it
                    if (string.IsNullOrWhiteSpace(muser.Oid))
                    {
                        Console.WriteLine("TRYING TO CREATE USER IN DB WITH THE OID: " + oidClaim.Value +
                                          " and the name: " + nameClaim.Value + " and the email: " + emailClaim.Value);
                        _logger.Log(LogLevel.Information,
                            $"{nameof(UpdateAppStateWithUserAsync)}: Creating new user, since the user does not exist with the oid: {oidClaim.Value}");
                        var tmuser = await _userWebRepository.CreateUserAsync(newMUserDto);
                        if (tmuser)
                        {
                            _logger.Log(LogLevel.Information,
                                $"{nameof(UpdateAppStateWithUserAsync)}: User created successfully and retrieved from database user with the oid: {oidClaim.Value}");
                            muser = await GetUserInformationFromDB(oidClaim.Value);
                        }
                    }

                    var newGlobalState = CreateGlobalStateFromMUser(muser);
                    if (string.IsNullOrWhiteSpace(muser.Oid))
                    {
                        // Updating tenant information
                        _logger.Log(LogLevel.Information,
                            $"{nameof(UpdateAppStateWithUserAsync)}: Updating tenant information based on the oid: {muser.Oid}");
                        var defaultTenant = await GetDefaultTenantByIdAsync(muser.DefaultTenantId);
                        var allMyTenants = await GetMyTenantsAsync(muser.Oid);

                        if (muser.DefaultTenantId != 0)
                        {
                            _logger.Log(LogLevel.Information,
                                $"{nameof(UpdateAppStateWithUserAsync)}: Setting default tenant id to: {muser.DefaultTenantId} and active tenant to: {muser.DefaultTenantId}");
                            newGlobalState.DefaultTenantId = muser.DefaultTenantId;
                            // Commented for GlobalStateTenant
                            // newGlobalState.ActiveTenant = muser.Tenants.FirstOrDefault(t => t.Id == muser.DefaultTenantId);
                        }

                        if (defaultTenant != null || defaultTenant.Id != 0 || newGlobalState.DefaultTenantId != 0)
                        {
                            _logger.Log(LogLevel.Information,
                                $"{nameof(UpdateAppStateWithUserAsync)}: Setting default tenant id to: {defaultTenant.Id} and active tenant to: {defaultTenant.Id}");
                            // Commented for GlobalStateTenant
                            //newGlobalState.ActiveTenant = defaultTenant;
                            newGlobalState.DefaultTenantId = defaultTenant.Id;
                        }

                        if (newGlobalState.ActiveTenant.Id == 0 && newGlobalState.DefaultTenantId == 0 && allMyTenants != null && allMyTenants.Count >= 1)
                        {
                            _logger.Log(LogLevel.Information,
                                $"{nameof(UpdateAppStateWithUserAsync)}: Setting default tenant id to: {allMyTenants[0].Id} and active tenant to: {allMyTenants[0].Id} since there is no default tenant id");
                            // get the first tenant from allMyTenants and set it as the default tenant
                            // Commented for GlobalStateTenant

                            //newGlobalState.ActiveTenant = allMyTenants[0];
                            newGlobalState.DefaultTenantId = allMyTenants[0].Id;
                            // Save the default tenant id to the database
                            await _userWebRepository.UpdateDefaultTenantId(newGlobalState.DefaultTenantId, muser.Oid);
                        }
                        // Commented for GlobalStateTenant
                        //if (allMyTenants != null && allMyTenants.Count > 0) newGlobalState.Tenants = allMyTenants;
                    }

                    newGlobalState.LastUpdated = DateTime.Now.ToUniversalTime();
                    _logger.Log(LogLevel.Information,
                        $"{nameof(UpdateAppStateWithUserAsync)}: Setting global state with the oid: {oidClaim.Value}");
                    await _localStorageService.SetItemAsync("globalState", newGlobalState);
                    OnGlobalStateChanged?.Invoke();
                }

                _userState.SetUser(muser);
            }
        }
        catch(Exception e)
        {
            _logger.Log(LogLevel.Error, e, $"{nameof(UpdateAppStateWithUserAsync)} failed with error message {e.Message}");
        }
    }

    // Creates a global state from the given MUser
    private static GlobalState CreateGlobalStateFromMUser(MUser muser)
    {
        return new GlobalState
        {
            Name = muser.Name,
            Email = muser.Email,
            Oid = muser.Oid,
            HasDatabaseUser = true,
            IsActivated = muser.HasBeenActivated,
            FirstLogin = muser.IsFirstLogin,
            DefaultTenantId = muser.DefaultTenantId,
            IsUpToDate = true
        };
    }

    public async Task UpdateDateAsync()
    {
        // Get the global state and update the date
        var globalState = await GetGlobalStateAsync();
        // Does the global state exist?
        if (globalState == null)
        {
            _logger.Log(LogLevel.Information,
                $"{nameof(UpdateDateAsync)}: The global state does not exist, creating a new one");
            globalState = new GlobalState();
        }
        globalState.LastUpdated = DateTime.Now.ToUniversalTime();
        await _localStorageService.SetItemAsync("globalState", globalState);
        OnGlobalStateChanged?.Invoke();
    }

    public async Task UpdateTenantStateAsync()
    {
        var globalState = await GetGlobalStateAsync();
        var muser = await GetUserInformationFromDB(globalState.Oid);
        var allMyTenants = await GetMyTenantsAsync(muser.Oid);
        
        //Console.WriteLine("mæ tenant neim is:" + allMyTenants.FirstOrDefault().Name);
        
        if (muser.DefaultTenantId != 0)
        {
            globalState.DefaultTenantId = muser.DefaultTenantId;
            // Commented for GlobalStateTenant

            //globalState.ActiveTenant = allMyTenants.FirstOrDefault(x => x.Id == muser.DefaultTenantId);
        }

        // Commented for GlobalStateTenant
        //if(allMyTenants != null && allMyTenants.Count > 0) globalState.Tenants = allMyTenants;
        
        // if allmytenants is not null and defaulttenantid is 0, set the first tenant as the default tenant
        if (allMyTenants != null && allMyTenants.Count >= 1 && globalState.DefaultTenantId == 0)
        {
            globalState.DefaultTenantId = allMyTenants[0].Id;
            // Commented for GlobalStateTenant

            //globalState.ActiveTenant = allMyTenants[0];
            await _userWebRepository.UpdateDefaultTenantId(globalState.DefaultTenantId, globalState.Oid);
        }
        
        await _localStorageService.SetItemAsync("globalState", globalState);
    }
    
    public async Task<Tenant> GetDefaultTenantByIdAsync(int defaultTenantId)
    {
        var tenant = new Tenant();

        // This will get the default tenant from the database
        if (defaultTenantId != null && defaultTenantId != 0)
        {
            _logger.Log(LogLevel.Information,
                $"{nameof(GetDefaultTenantByIdAsync)}: Getting default tenant with the id: {defaultTenantId}");
            tenant = await _tenantWebRepository.GetTenantById(defaultTenantId);
        }

        return tenant;
    }

    public async Task<List<Tenant>> GetMyTenantsAsync(string oid)
    {
        try
        {
            Console.WriteLine("Getting my tenants for the oid: " + oid);
            _logger.Log(LogLevel.Information,
                $"{nameof(GetMyTenantsAsync)}: Getting all tenants for the oid: {oid}");
            var results = await _tenantWebRepository.GetAllTenantsByOidAsync(oid);
            if (results != null)
                return results;
            else
                return new List<Tenant>();
        }
        catch(Exception e){
            _logger.Log(LogLevel.Error, e, $"{nameof(GetMyTenantsAsync)} failed. Error message: {e.Message}");
            Console.WriteLine("Error getting tenants: " + e.Message);
            return new List<Tenant>();
        }
    }

    // Event that gets triggered when the global state changes
    public event Action OnGlobalStateChanged;

    // Retrieves the user information from the database
    public async Task<MUser> GetUserInformationFromDB(string oid)
    {
        Console.WriteLine("Getting user information from database with the oid: " + oid);
        // Get the user information from the database
        var user = await _userWebRepository.GetUserByOid(oid);

        if (!string.IsNullOrEmpty(user?.Email))
        {
            _logger.Log(LogLevel.Information,
                $"{nameof(GetUserInformationFromDB)}: User retrieved from database with the oid: {oid}");
            return user;
        }
        else
        {
            _logger.Log(LogLevel.Warning,
                $"{nameof(GetUserInformationFromDB)}: User does not exist with the oid: {oid}");
            return new MUser();
        }
    }

    public async Task<GlobalState> GetGlobalStateAsync()
    {
        _logger.Log(LogLevel.Information, $"{nameof(GetGlobalStateAsync)}: Getting global state");
        var state = await _localStorageService.GetItemAsync<GlobalState>("globalState");

        return state;
    }
}