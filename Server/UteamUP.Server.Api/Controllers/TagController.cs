using UteamUP.Server.Controllers;

namespace UteamUP.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TagController : ControllerBase
{
    private readonly ILogger<PlanController> _logger;
    private readonly ITagRepository _tag;
    private readonly IMUserRepository _user;
    
    public TagController(ILogger<PlanController> logger, ITagRepository tag, IMUserRepository user)
    {
        _logger = logger;
        _tag = tag;
        _user = user;
    }
    
    // Create the tag
    [HttpPost]
    public async Task<IActionResult> CreateTagAsync([FromBody] List<TagDto> tags)
    {
        // Create the tag
        var result = await _tag.CreateAsync(tags);
        if(result == null) return BadRequest("Something went wrong while creating the tag, please review logs for more information");
        
        return Ok(result);
    }
}