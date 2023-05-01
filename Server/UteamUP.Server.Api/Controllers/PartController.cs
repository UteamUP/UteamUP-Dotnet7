namespace UteamUP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PartController : ControllerBase
{
    private readonly ILogger<PartController> _logger;
    private readonly IPartRepository _part;

    public PartController(IPartRepository part, ILogger<PartController> logger)
    {
        _part = part;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}