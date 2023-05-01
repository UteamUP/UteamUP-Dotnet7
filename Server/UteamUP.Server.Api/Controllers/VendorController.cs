namespace UteamUP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class VendorController : ControllerBase
{
    private readonly ILogger<VendorController> _logger;
    private readonly IVendorRepository _vendor;

    public VendorController(IVendorRepository vendor, ILogger<VendorController> logger)
    {
        _vendor = vendor;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}