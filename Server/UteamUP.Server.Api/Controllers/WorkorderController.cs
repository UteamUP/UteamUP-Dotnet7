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

    
    // GET: api/workorders
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            // Get all workorders
            //var workorders = await _workorder.GetWorkordersAsync();
            // return ok
            //return Ok(workorders);
            return Ok();
        }
        catch (Exception ex)
        {
            // log the error and return 500
            _logger.LogError(ex, "Error getting workorders");
            return StatusCode(StatusCodes.Status500InternalServerError, "Error getting workorders");
        }
    }
}