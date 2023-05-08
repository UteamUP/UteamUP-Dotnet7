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
        _logger = logger;
    }

    public async Task<List<Tenant>> GetAllTenantsAsyncByOid(string oid)
    {
        // Get all tenants by oid
        var tenants = await _context.Tenants.Where(x => x.Users.Any(y => y.Oid == oid)).ToListAsync();

        // Check if the tenants are null
        if (tenants == null)
        {
            _logger.Log(LogLevel.Warning, $"{nameof(GetAllTenantsAsyncByOid)}: No tenants found");
            return new List<Tenant>();
        }

        return tenants;
    }

    public async Task<Tenant?> CreateTenantAsync(TenantDto tenant, string oid)
    {
        // Check if tenant is null
        if (string.IsNullOrWhiteSpace(tenant.Name))
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateTenantAsync)}: Tenant is null");
            return new Tenant();
        }
        
        // Check if user is null
        if (string.IsNullOrWhiteSpace(oid))
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateTenantAsync)}: Oid is null");
            return new Tenant();
        }
        
        // Get user by oid
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Oid == oid);
        if (string.IsNullOrWhiteSpace(user?.Oid))
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateTenantAsync)}: User is null");
            return new Tenant();
        }
        
        // Map tenantdto to tenant
        var mappedTenant = _mapper.Map<Tenant>(tenant);

        try
        {
            // Assign user to tenant
            mappedTenant.OwnerId = user.Id;

            // Update timestamps
            mappedTenant.CreatedAt = DateTime.Now.ToUniversalTime();
            mappedTenant.UpdatedAt = DateTime.Now.ToUniversalTime();

            // Add user to tenant
            mappedTenant.Users.Add(user);

            // Add tenant to database
            _context.Tenants.Add(mappedTenant);

            // Save changes
            await _context.SaveChangesAsync();

            // Return tenant
            _logger.Log(LogLevel.Information,
                $"{nameof(CreateTenantAsync)}: Tenant created successfully with id {{MappedTenantId}} and name {{MappedTenantName}}",
                mappedTenant.Id, mappedTenant.Name);

            return mappedTenant;
        }catch(Exception e)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateTenantAsync)}: {e.Message}");
            return new Tenant();
        }
    }

    public async Task<List<Tenant>> GetInvitesAsync(string oid)
    {
        // Get the user by oid
        var user = _context.Users.Where(a => a.Oid == oid).FirstOrDefault();
        
        // if the user is null return emtpy list
        if (user == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetInvitesAsync)}: User is null");
            return new List<Tenant>();
        }
        
        // Get all invites by oid
        var invites = _context.InvitedUsers.Where(x => x.Email == user.Email).ToListAsync();
        
        // if the invites are null return empty list
        if (invites == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetInvitesAsync)}: Invites are null");
            return new List<Tenant>();
        }
        
        // Move all data from invites list to tenants list
        var tenants = new List<Tenant>();
        foreach (var invite in invites.Result)
        {
            var tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == invite.TenantId);
            if (tenant != null)
            {
                tenants.Add(tenant);
            }
        }
        
        // Return tenants
        return tenants;
    }

    private bool TenantExists(int id)
    {
        return _context.Tenants.Any(e => e.Id == id);
    }
}