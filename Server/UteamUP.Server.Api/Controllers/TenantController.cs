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
}