namespace UteamUP.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class WorkorderController : ControllerBase
{
    private readonly ILogger<WorkorderController> _logger;
    private readonly IWorkorderRepository _workorder;

    public WorkorderController(IWorkorderRepository workorder, ILogger<WorkorderController> logger)
    {
        _workorder = workorder;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}