namespace UteamUP.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SubscriptionController : ControllerBase
{
    private readonly ILogger<SubscriptionController> _logger;
    private readonly ISubscriptionRepository _subscription;

    public SubscriptionController(ISubscriptionRepository subscription, ILogger<SubscriptionController> logger)
    {
        _subscription = subscription;
        _logger = logger;
    }

    [HttpPost("add/{tenantId}/{planId}/{extraLicenses}")]
    public async Task<IActionResult> CreateAsync(int tenantId, int planId, int extraLicenses)
    {
        var result = await _subscription.CreateAsync(tenantId, planId, extraLicenses);
        if (result == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateAsync)}: Something went wrong while creating the subscription");
            return BadRequest("Something went wrong while creating the subscription, please review logs for more information");
        }
        
        return Ok(result);
    }
    
    [HttpGet("tenant/{tenantId}")]
    public async Task<IActionResult> GetByTenantIdAsync(int tenantId)
    {
        var result = await _subscription.GetByTenantIdAsync(tenantId);
        if (result == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetByTenantIdAsync)}: Something went wrong while getting the subscription");
            return BadRequest("Something went wrong while getting the subscription, please review logs for more information");
        }
        
        return Ok(result);
    }
}