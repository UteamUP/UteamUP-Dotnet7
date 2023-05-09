using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UteamUP.Client.Web.Repository.Interfaces;
using UteamUP.Shared.States;
using Blazored.LocalStorage;

namespace UteamUP.Client.Web.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IUserWebRepository _userWebRepository;
        private readonly ITenantWebRepository _tenantWebRepository;
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly UserState _userState;

        public CustomAuthenticationStateProvider(
            ILocalStorageService localStorageService,
            IUserWebRepository userWebRepository,
            IAccessTokenProvider accessTokenProvider, UserState userState, ITenantWebRepository tenantWebRepository)
        {
            _localStorageService = localStorageService;
            _userWebRepository = userWebRepository;
            _accessTokenProvider = accessTokenProvider;
            _userState = userState;
            _tenantWebRepository = tenantWebRepository;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity;
            var claims = Enumerable.Empty<Claim>();
            var savedStateJson = await _localStorageService.GetItemAsStringAsync("globalState");
            OnGlobalStateChanged?.Invoke();
            
            if (!string.IsNullOrEmpty(savedStateJson))
            {
                Console.WriteLine("Found saved state");
                GlobalState globalState = await _localStorageService.GetItemAsync<GlobalState>("globalState");
                claims = new[]
                {
                    new Claim("name", globalState.Name),
                    new Claim("email", globalState.Email),
                    new Claim("oid", globalState.Oid)
                };
            }
            else
            {
                Console.WriteLine("No saved state, requesting token");
                var tokenResult = await _accessTokenProvider.RequestAccessToken();

                if (tokenResult.TryGetToken(out var accessToken))
                {
                    Console.WriteLine("Got token");
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(accessToken.Value);

                    var name = jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                    var email = jwtToken.Claims.FirstOrDefault(c => c.Type == "signInNames.emailAddress")?.Value;
                    var oid = jwtToken.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
                    Console.WriteLine("Got claims from token, oid: " + oid + " email: " + email + " name: " + name);

                    if (!string.IsNullOrEmpty(oid))
                    {
                        Console.WriteLine("Building Claims");
                        claims = new[]
                        {
                            new Claim("name", name),
                            new Claim("email", email),
                            new Claim("oid", oid)
                        };
                    }
                }
            }
            
            identity = new ClaimsIdentity(claims, "Authentication");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public async Task UpdateAppStateWithUserAsync(ClaimsPrincipal user)
        {
            var oidClaim = user.Claims.FirstOrDefault(c => c.Type == "oid");
            var nameClaim = user.Claims.FirstOrDefault(c => c.Type == "name");
            var emailClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

            if (emailClaim?.Value == null)
                emailClaim = user.Claims.FirstOrDefault(c => c.Type == "signInNames.emailAddress");
            if (emailClaim?.Value == null)
                emailClaim = user.Claims.FirstOrDefault(c => c.Type == "email");

            if (oidClaim != null)
            {
                MUser getUser = await GetUserInformationFromDB(oidClaim.Value);

                // Check if the global state exists
                GlobalState globalState = await _localStorageService.GetItemAsync<GlobalState>("globalState");

                // if the global state does not exist, create it and update it with the user information
                if (globalState == null)
                {
                    var muser = await GetUserInformationFromDB(oidClaim.Value);
                    Console.WriteLine("Got DB user");
                    
                    MUserDto newMUserDto = new MUserDto
                    {
                        Name = nameClaim?.Value,
                        Email = emailClaim?.Value,
                        Oid = oidClaim.Value,
                    };
                    Console.WriteLine("Creating new MUserDto with the following details oid: " + oidClaim.Value + " name: " + nameClaim?.Value + " email: " + emailClaim?.Value);
                    // if the muser is null, create a new user and return it
                    if (muser == null)
                    {
                        Console.WriteLine("MUser is null, creating new user");
                        var tmuser = await _userWebRepository.CreateUserAsync(newMUserDto);
                        if(tmuser)
                        {
                            Console.WriteLine("User has been created");
                            muser = await GetUserInformationFromDB(oidClaim.Value);
                        }
                    }
                    
                    Console.WriteLine("Getting Tenants");
                    //var tenants = await _tenantWebRepository.GetMyTenantsAsync(oidClaim.Value);
                    Console.WriteLine("Getting Invites");
                    //var invites = await _tenantWebRepository.GetInvitesAsync(oidClaim.Value);

                    Console.WriteLine("Creating GlobalState");
                    GlobalState newGlobalState = new GlobalState
                    {
                        Name = muser.Name,
                        Email = muser.Email,
                        Oid = muser.Oid,
                        HasDatabaseUser = true,
                        IsActivated = muser.HasBeenActivated,
                        FirstLogin = muser.IsFirstLogin,
                        DefaultTenantId = muser.DefaultTenantId,

                        /*
                        if(tenants != null) Tenants = tenants,
                        if(TenantsInvited != null) TenantsInvited = invites,
                        if(TenantsInvited != null) HasTenantInvites = invites.Count > 0
                        */
                    };
                    
                    await _localStorageService.SetItemAsync("globalState", newGlobalState);
                    OnGlobalStateChanged?.Invoke();
                    // Let the page know that the global state has been created or updated
                    Console.WriteLine("GlobalState has been created in local storage");
                }

                _userState.SetUser(getUser);
            }
            else
            {
                Console.WriteLine("User claims are empty or not found");
            }
        }

        public event Action OnGlobalStateChanged;
        public async Task<MUser> GetUserInformationFromDB(string oid)
        {
            // Get the user information from the database
            MUser? user = await _userWebRepository.GetUserByOid(oid);

            if (!string.IsNullOrEmpty(user?.Email))
            {
                return user;
            }
            else
            {
                return new MUser();
            }
        }

        // Add a method to retrieve the GlobalState
        public void GetGlobalState()
        {
            // Your logic to return the GlobalState
        }
    }
}
