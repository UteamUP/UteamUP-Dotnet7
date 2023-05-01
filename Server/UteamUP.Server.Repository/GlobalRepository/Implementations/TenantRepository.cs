namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class TenantRepository : ITenantRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<TenantRepository> _logger;

    public TenantRepository(pgContext context, IMapper mapper, ILogger<TenantRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger ?? NullLogger<TenantRepository>.Instance;
    }

    public async Task<List<Tenant>> GetAllTenantsAsyncByOid(string oid)
    {
        // Get all tenants by oid
        var tenants = await _context.Tenants.Where(x => x.Users.Any(y => y.Oid == oid)).ToListAsync();

        // Check if the tenants are null
        if (tenants == null)
        {
            _logger.Log(LogLevel.Warning, $"No tenants found");
            return new List<Tenant>();
        }

        return tenants;
    }

    public async Task<List<InvitedUser>> GetTenantInvitesAsync(string email)
    {
        // Get all invites by email
        var invites = await _context.InvitedUsers.Where(x => x.Email == email).ToListAsync();
        // Check if the tenants are null

        if (invites == null)
        {
            _logger.Log(LogLevel.Warning, $"No invites found");
            return new List<InvitedUser>();
        }

        return invites;
    }

    private bool TenantExists(int id)
    {
        return _context.Tenants.Any(e => e.Id == id);
    }
}