namespace UteamUP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TenantController : ControllerBase
{
    private readonly ILogger<TenantController> _logger;
    private readonly ITenantRepository _tenant;
    private readonly ISubscriptionRepository _subscription;

    public TenantController(
        ITenantRepository tenant, 
        ILogger<TenantController> logger, 
        ISubscriptionRepository subscription)
    {
        _tenant = tenant;
        _logger = logger;
        _subscription = subscription;
    }
    
    private async Task<MUserDto> ValidateUser()
    {
        // Get the oid from the user who is logged in
        var oid = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? User.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
        // Get email from the user who is logged in
        var email = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/signInNames.emailAddress")?.Value ?? User.Claims.FirstOrDefault(c => c.Type == "signInNames.emailAddress")?.Value;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(oid))
            return new MUserDto();

        MUserDto mUser = new MUserDto();
        mUser.Email = email;
        mUser.Oid = oid;
        
        // Validate that the user is admin
        return mUser;
    }

    [HttpGet("oid/{oid}")]
    public async Task<IActionResult> GetByOidAsync(string oid)
    {
        List<Tenant> tenantList = new();

        if (string.IsNullOrWhiteSpace(oid))
        {
            _logger.Log(LogLevel.Error, $"Oid is null or empty");
            return NotFound(tenantList);
        }

        _logger.Log(LogLevel.Information, $"Getting user by oid {oid}");
        var tenants = await _tenant.GetAllTenantsByOidAsync(oid);
        if (tenants.Count == 0)
        {
            _logger.Log(LogLevel.Information, $"No tenants found for user with oid {oid}");
            return Ok(tenantList);
        }

        return Ok(tenants);
    }

    // Create tenant
    [HttpPost("add/{planId}/{extraLicenses}")]
    public async Task<IActionResult> CreateTenantAsync(TenantDto tenant, int planId, int extraLicenses)
    {
        /*
        if (tenant == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateTenantAsync)}: Tenant is null");
            return BadRequest();
        }
        if (string.IsNullOrWhiteSpace(user.Oid))
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateTenantAsync)}: User is null");
            return BadRequest();
        }
        */
        var user = await ValidateUser();

        _logger.Log(LogLevel.Information, $"{nameof(CreateTenantAsync)}: Creating tenant");
        var createdTenant = await _tenant.CreateTenantAsync(tenant, user.Oid, planId, extraLicenses);
        /*
        if (string.IsNullOrWhiteSpace(createdTenant.Name))
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateTenantAsync)}: Tenant could not be created");
            return BadRequest();
        }
        // Create subscription for the tenant
        var subscription = await _subscription.CreateAsync(createdTenant.Id, planId, extraLicenses);
        if (subscription == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateTenantAsync)}: Subscription could not be created");
            return BadRequest();
        }
        */
        return Ok(createdTenant);
    }
    
    // Get all tenant invites for user oid
    [HttpGet("invites/{oid}")]
    public async Task<IActionResult> GetInvitesByOidAsync(string oid)
    {
        var user = await ValidateUser();
        // Check if the oid is the same as the user oid
        if (user.Oid != oid)
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetInvitesByOidAsync)}: Could not validate user");
            return BadRequest("Could not validate user");
        }

        List<Tenant> tenantList = new();

        if (string.IsNullOrWhiteSpace(oid))
        {
            _logger.Log(LogLevel.Error, $"Oid is null or empty");
            return NotFound(tenantList);
        }

        _logger.Log(LogLevel.Information, $"Getting user by oid {oid}");
        var tenants = await _tenant.GetInvitesAsync(oid);
        if (tenants.Count == 0)
        {
            _logger.Log(LogLevel.Information, $"No tenant invites found for user with oid {oid}");
            return Ok(tenantList);
        }

        return Ok(tenants);
    }
    
    // Get all my owned tenants
    [HttpGet("owned/{oid}")]
    public async Task<IActionResult> GetOwnedTenantsAsync(string oid)
    {
        var user = await ValidateUser();
        // Check if the oid is the same as the user oid
        if (user.Oid != oid)
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetOwnedTenantsAsync)}: Could not validate user");
            return BadRequest("Could not validate user");
        }

        List<Tenant> tenantList = new();

        if (string.IsNullOrWhiteSpace(oid))
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetOwnedTenantsAsync)}: Oid is null or empty");
            return NotFound(tenantList);
        }

        _logger.Log(LogLevel.Information, $"{nameof(GetOwnedTenantsAsync)}: Getting user by oid {oid}");
        var tenants = await _tenant.GetOwnedTenantsAsync(oid);
        if (tenants.Count == 0)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetOwnedTenantsAsync)}: No owned tenants found for user with oid {oid}");
            return Ok(tenantList);
        }

        return Ok(tenants);
    }
    
    // Get tenant by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTenantByIdAsync(int id)
    {
        if (id <= 0)
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetTenantByIdAsync)}: Id is null or empty");
            return NotFound();
        }

        _logger.Log(LogLevel.Information, $"{nameof(GetTenantByIdAsync)}: Getting tenant by id {id}");
        var tenant = await _tenant.GetTenantById(id.ToString());
        if (tenant == null)
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetTenantByIdAsync)}: No tenant found for id {id}");
            return NotFound();
        }

        return Ok(tenant);
    }
}