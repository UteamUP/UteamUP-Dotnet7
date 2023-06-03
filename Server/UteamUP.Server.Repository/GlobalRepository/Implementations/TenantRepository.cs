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

    public async Task<List<Tenant>> GetAllTenantsByOidAsync(string oid)
    {
        // Get all tenants by oid
        var tenants = await _context.Tenants.Where(x => x.Users.Any(y => y.Oid == oid)).ToListAsync();

        // Check if the tenants are null
        if (tenants == null)
        {
            _logger.Log(LogLevel.Warning, $"{nameof(GetAllTenantsByOidAsync)}: No tenants found");
            return new List<Tenant>();
        }

        return tenants;
    }

    public async Task<Tenant?> CreateTenantAsync(TenantDto tenant, string oid, int planId, int extraLicenses)
    {
        
        // Get the plan by id
        var plan = await _context.Plans.FirstOrDefaultAsync(x => x.Id == planId);
        
        // Get user by oid
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Oid == oid);
        
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
            await _context.SaveChangesAsync();

            // Get the tenant
            var tenantFromDb = await _context.Tenants.FirstOrDefaultAsync(x => x.Name == mappedTenant.Name && x.OwnerId == user.Id);

            // Create subscription for the tenant
            var subscription = new Subscription
            {
                TenantId = tenantFromDb.Id,
                PlanId = planId,
                ExtraAmountOfLicenses = extraLicenses,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                UpdatedAt = DateTime.Now.ToUniversalTime()
            };
            
            // Add subscription to database
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            
            // Get the subscription from the database
            var subscriptionFromDb = await _context.Subscriptions.FirstOrDefaultAsync(x => x.TenantId == tenantFromDb.Id);
            int totalLicenses = plan.LicenseIncluded + extraLicenses;
            
            // Create a license for the tenant
            var license = new License
            {
                SubscriptionId = subscriptionFromDb.Id,
                MaxLicenses = totalLicenses,
                MinLicenses = plan.LicenseIncluded,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                UpdatedAt = DateTime.Now.ToUniversalTime()
            };
            
            // Add license to database
            await _context.Licenses.AddAsync(license);
            
            // Save changes
            await _context.SaveChangesAsync();
            
            // Get the license from database
            var licenseFromDb = await _context.Licenses.FirstOrDefaultAsync(x => x.SubscriptionId == subscriptionFromDb.Id);
            
            // Add users to the license
            _context.LicenseUsers.Add(new LicenseUsers
            {
                LicenseId = licenseFromDb.Id,
                MUserId = user.Id,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                UpdatedAt = DateTime.Now.ToUniversalTime()
            });

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

    public async Task<List<Tenant>> GetOwnedTenantsAsync(string oid)
    {
        // Check if user exists
        if (string.IsNullOrWhiteSpace(oid))
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetOwnedTenantsAsync)}: Oid is null");
            return new List<Tenant>();
        }
        
        // Get user by oid
        var user = await _context.Users.Where(a => a.Oid == oid).FirstOrDefaultAsync();

        // Check if user exists
        if (user == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetOwnedTenantsAsync)}: User is null");
            return new List<Tenant>();
        }
        
        // Get all tenants by owner id
        var tenants = await _context.Tenants.Where(x => x.OwnerId == user.Id).ToListAsync();
        
        // Check if tenants are null
        if (tenants == null)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetOwnedTenantsAsync)}: You don't own any tenants");
            return new List<Tenant>();
        }
        
        // Return tenants
        return tenants;
    }

    public async Task<Tenant> GetTenantById(string tenantId)
    {
        // Check if tenant id is null
        if (string.IsNullOrWhiteSpace(tenantId))
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetTenantById)}: Tenant id is null");
            return new Tenant();
        }
        
        // Get tenant by id
        var tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == int.Parse(tenantId));
        return tenant ?? new Tenant();
    }

    public async Task<Tenant> UpdateTenantByIdAsync(Tenant tenant, int planId, int extraLicenses)
    {
        if(planId == 0 || planId == null || extraLicenses == 0 || extraLicenses == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(UpdateTenantByIdAsync)}: PlanId or extraLicenses is null");
            return new Tenant();
        }
        
        // Get the current tenant
        var tenantFromDb = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == tenant.Id);
        
        // Get the current plan by planId
        var plan = await _context.Plans.FirstOrDefaultAsync(x => x.Id == planId);
        
        // Confirm that plan exists
        if (plan == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(UpdateTenantByIdAsync)}: Plan is null");
            return new Tenant();
        }
        
        // Get the subscription that is assigned to the tenant
        var subscription = await _context.Subscriptions.FirstOrDefaultAsync(x => x.TenantId == tenantFromDb.Id);
        
        // Assign the new plan to the subscription
        subscription.PlanId = planId;
        
        // Assign the new extra licenses to the subscription
        subscription.ExtraAmountOfLicenses = extraLicenses;
        
        // Update subscription datetime
        subscription.UpdatedAt = DateTime.Now.ToUniversalTime();
        
        // Update the subscription
        _context.Subscriptions.Update(subscription);
        
        // Get the licenses that is assigned to the subscription
        var license = await _context.Licenses.FirstOrDefaultAsync(x => x.SubscriptionId == subscription.Id);
        
        // Calculate the total amount of licenses
        var totalLicenses = plan.LicenseIncluded + extraLicenses;
        
        // Assign new subscription to the license
        license.SubscriptionId = subscription.Id;
        
        // Update the license
        license.UpdatedAt = DateTime.Now.ToUniversalTime();
        
        // Update max licenses in the license
        license.MaxLicenses = totalLicenses;
        
        // Update the license
        _context.Licenses.Update(license);

        // Update the tenant UpdatedAt
        tenant.UpdatedAt = DateTime.Now.ToUniversalTime();
        
        // Update the tenant based on the new tenant object
        _context.Tenants.Update(tenant);
        
        // Save changes
        await _context.SaveChangesAsync();
        
        // Get the tenant from the database and return it
        var tenantFromDb2 = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == tenant.Id);
        
        // Return the tenant
        return tenantFromDb2 ?? new Tenant();
    }


    private bool TenantExists(int id)
    {
        return _context.Tenants.Any(e => e.Id == id);
    }
}