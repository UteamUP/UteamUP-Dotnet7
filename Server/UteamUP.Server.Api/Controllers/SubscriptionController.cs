namespace UteamUP.Server.Controllers;

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

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}