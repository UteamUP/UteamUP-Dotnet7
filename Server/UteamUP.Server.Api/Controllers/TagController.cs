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
    
    // Get Tag by name
    [HttpGet("{name}")]
    public async Task<IActionResult> GetTagByNameAsync(string name)
    {
        // Get the tag
        var result = await _tag.GetTagByNameAsync(name);
        if(result == null) return NotFound("Tag not found");
        
        return Ok(result);
    }
    
    // Get Tag by name and tenant id
    [HttpGet("{name}/tenant/{tenantId}")]
    public async Task<IActionResult> GetTagByNameAndTenantIdAsync(string name, int tenantId)
    {
        // Get the tag
        var result = await _tag.GetTagByNameAndTenantIdAsync(name, tenantId);
        if(result == null) return NotFound("Tag not found");
        
        return Ok(result);
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
    
    // Get tag by name and location name
    [HttpGet("{name}/{locationName}")]
    public async Task<IActionResult> GetTagByNameAndLocationNameAsync(string name, string locationName)
    {
        // Get the tag
        var result = await _tag.GetTagByNameAndLocationNameAsync(name, locationName);
        if(result == null) return NotFound("Tag not found");
        
        return Ok(result);
    }
}