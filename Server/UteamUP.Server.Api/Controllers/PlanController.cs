namespace UteamUP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PlanController : ControllerBase
{
    private readonly ILogger<PlanController> _logger;
    private readonly IPlanRepository _plan;

    public PlanController(IPlanRepository plan, ILogger<PlanController> logger)
    {
        _plan = plan;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}