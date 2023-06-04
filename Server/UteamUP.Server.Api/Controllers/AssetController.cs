namespace UteamUP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AssetController : ControllerBase
{
    private readonly IAssetRepository _asset;
    private readonly ILogger<AssetController> _logger;

    public AssetController(IAssetRepository asset, ILogger<AssetController> logger)
    {
        _asset = asset;
        _logger = logger;
    }

    [HttpPost("{text}")]
    public async Task<IActionResult> Create(string text)
    {
        return Ok(text);
    }
}