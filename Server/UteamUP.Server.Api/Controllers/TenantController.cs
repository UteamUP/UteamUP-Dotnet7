namespace UteamUP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TenantController : ControllerBase
{
    private readonly ILogger<TenantController> _logger;
    private readonly ITenantRepository _tenant;

    public TenantController(ITenantRepository tenant, ILogger<TenantController> logger)
    {
        _tenant = tenant;
        _logger = logger;
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
        var tenants = await _tenant.GetAllTenantsAsyncByOid(oid);
        if (tenants.Count == 0)
        {
            _logger.Log(LogLevel.Information, $"No tenants found for user with oid {oid}");
            return Ok(tenantList);
        }

        return Ok(tenants);
    }

    [HttpGet("invite/{email}")]
    public async Task<IActionResult> GetByInviteAsync(string email)
    {
        List<Tenant> tenantList = new();

        if (string.IsNullOrWhiteSpace(email))
        {
            _logger.Log(LogLevel.Error, $"Email is null or empty");
            return NotFound(tenantList);
        }

        _logger.Log(LogLevel.Information, $"Getting user by email {email}");
        var tenants = await _tenant.GetTenantInvitesAsync(email);
        if (tenants.Count == 0)
        {
            _logger.Log(LogLevel.Information, $"No tenants found for user with email {email}");
            return Ok(tenantList);
        }

        return Ok(tenants);
    }
    
    // Create tenant
    [HttpPost]
    public async Task<IActionResult> CreateTenantAsync([FromBody] TenantDto tenant)
    {
        if (tenant == null)
        {
            _logger.Log(LogLevel.Error, $"CreateTenantAsync: Tenant is null");
            return BadRequest();
        }

        var user = await ValidateUser();
        if (string.IsNullOrWhiteSpace(user.Oid))
        {
            _logger.Log(LogLevel.Error, $"CreateTenantAsync: User is null");
            return BadRequest();
        }
        
        _logger.Log(LogLevel.Information, $"CreateTenantAsync: Creating tenant");
        var createdTenant = await _tenant.CreateTenantAsync(tenant, user.Oid);
        if (string.IsNullOrWhiteSpace(createdTenant.Name))
        {
            _logger.Log(LogLevel.Error, $"CreateTenantAsync: Tenant could not be created");
            return BadRequest();
        }

        return Ok(createdTenant);
    }
}