using UteamUP.Shared.States;

namespace UteamUP.Server.Api.Helpers;

public class ProfileBuilder : IProfileBuilder
{
    private readonly IMapper _mapper;
    private readonly ILogger<ProfileBuilder> _logger;

    private IMUserRepository _userRepository;
    private ITenantRepository _tenantRepository;
    
    public ProfileBuilder(
        IMapper mapper, 
        ILogger<ProfileBuilder> logger,
        IMUserRepository userRepository,
        ITenantRepository tenantRepository
    )
    {
        _mapper = mapper;
        _logger = logger;
        _userRepository = userRepository;
        _tenantRepository = tenantRepository;
    }

    public async Task<GlobalState?> GetUserProfile(string oid)
    {
        // if the oid is empty throw an exception
        if (string.IsNullOrWhiteSpace(oid))
        {
            _logger.Log(LogLevel.Warning, $"{nameof(GetUserProfile)}: Oid is null or empty");
            return new GlobalState();
        }
        
        // Check if the user state already exists
        
        // Get the user
        _logger.Log(LogLevel.Information, $"{nameof(GetUserProfile)}: Trying to get user by oid {oid}");
        var user = await _userRepository.GetByOidAsync(oid);
        if (string.IsNullOrWhiteSpace(user?.Oid))
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetUserProfile)}: User not found");
            return new GlobalState();
        }
        
        // Get the invites
        var invites = await _tenantRepository.GetInvitesAsync(oid);
        // Map invites to GlobalStateTenant list
        var invitesMapped = _mapper.Map<List<GlobalStateTenant>>(invites);
        
        
        // Get all the user tenasnts
        var tenants = await _tenantRepository.GetAllTenantsByOidAsync(oid);
        // Map tenants to GlobalStateTenant list
        var tenantsMapped = _mapper.Map<List<GlobalStateTenant>>(tenants);
        
        // Build the profile
        GlobalState globalState = new GlobalState();

        globalState.Oid = user.Oid;
        globalState.Email = user.Email;
        globalState.Name = user.Name;
        globalState.IsActivated = user.HasBeenActivated;
        globalState.HasDatabaseUser = true;
        globalState.HasTenantInvites = invites.Any();
        // Update with time now
        globalState.LastUpdated = DateTime.Now.ToUniversalTime();
        
        // Map the tenants to global state by creating a map first
        
        globalState.TenantsInvited = invitesMapped;
        globalState.Tenants = tenantsMapped;
        
        globalState.DefaultTenantId = user.DefaultTenantId;
        // if the default tenant is null set the tenant id to to first tenant in the tenants variable if there is any tenants
        if (globalState.DefaultTenantId == 0 && tenants.Any())
        {
            globalState.DefaultTenantId = tenants.First().Id;
        }

        globalState.ActiveTenant = tenantsMapped.FirstOrDefault(t => t.Id == globalState.DefaultTenantId);
        
        return globalState;
    }

    public async Task<GlobalState?> UpdateUserProfile(GlobalState globalState, string oid)
    {
        // if the oid is empty throw an exception
        if (string.IsNullOrWhiteSpace(oid))
        {
            _logger.Log(LogLevel.Warning, $"{nameof(GetUserProfile)}: Oid is null or empty");
            return new GlobalState();
        }
        
        // Check if the user state is empty
        if (globalState == null)
        {
            _logger.Log(LogLevel.Warning, $"{nameof(GetUserProfile)}: GlobalState is null");
            return new GlobalState();
        }
        
        // Update the state to now
        globalState.LastUpdated = DateTime.Now.ToUniversalTime();

        // Get invites
        var invites = await _tenantRepository.GetInvitesAsync(oid);
        // Map invites to GlobalStateTenant list
        var invitesMapped = _mapper.Map<List<GlobalStateTenant>>(invites);
        
        // Get all the user tenasnts
        var tenants = await _tenantRepository.GetAllTenantsByOidAsync(oid);
        // Map tenants to GlobalStateTenant list
        var tenantsMapped = _mapper.Map<List<GlobalStateTenant>>(tenants);
        
        // Build the profile
        GlobalState globalStateUpdated = new GlobalState();
        
        // Replace the invites and tenants to the new ones
        globalStateUpdated.TenantsInvited = invitesMapped;
        globalStateUpdated.Tenants = tenantsMapped;
        globalState.HasTenantInvites = invites.Any();

        // Return the updated state
        return globalStateUpdated;
    }
}