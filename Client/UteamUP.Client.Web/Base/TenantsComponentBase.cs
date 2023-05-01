using AutoMapper;

namespace UteamUP.Client.Web.Base;

public class TenantsComponentBase
{
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<TenantsComponentBase> _logger;
    private readonly IMapper _mapper;

    public TenantsComponentBase(IDispatcher dispatcher, ILogger<TenantsComponentBase> logger, IMapper mapper)
    {
        _dispatcher = dispatcher;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<bool> SetTenantsDetails(List<Tenant> tenants)
    {
        try
        {
            var tenantDtos = _mapper.Map<List<Tenant>, List<TenantDto>>(tenants);

            _logger.Log(LogLevel.Information, "Save the tenants to the global state");
            _dispatcher.Dispatch(new SetTenantsAction(tenantDtos));
            return true;
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return false;
        }
    }

    public async Task<bool> SetHasTenantInvites(bool hasTenantInvites, List<InvitedUser> tenantsInvited)
    {
        try
        {
            var tenantsInvitedDtos = _mapper.Map<List<InvitedUser>, List<InvitedUserDto>>(tenantsInvited);

            _logger.Log(LogLevel.Information, $"Check if user has tenant invites : {hasTenantInvites}");
            _dispatcher.Dispatch(new SetUserInviteAction(hasTenantInvites, tenantsInvitedDtos));

            return hasTenantInvites;
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return false;
        }
    }
}