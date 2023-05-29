using UteamUP.Server.Controllers;
using UteamUP.Server.Repository.GenericRepository.Interfaces;

namespace UteamUP.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TagController : ControllerBase
{
    private readonly ILogger<PlanController> _logger;
    private readonly ITagRepository _tag;
    private readonly IMUserRepository _user;
    private readonly IRepository<Tag> _tagRepository;

    
    public TagController(ILogger<PlanController> logger, ITagRepository tag, IMUserRepository user, IRepository<Tag> tagRepository)
    {
        _logger = logger;
        _tag = tag;
        _user = user;
        _tagRepository = tagRepository;
    }
    
    // Get Tag by name
    [HttpGet("name/{name}/tenant/{tenantId}")]
    public async Task<IActionResult> GetTagByNameAsync(string name, int tenantId)
    {
        Console.WriteLine("Trying to find tag by name: " + name + " and tenant id: " + tenantId);
        // Get the tag
        //return Ok(_tagRepository.GetByNameAndTenantId(name, tenantId));
        return Ok(await _tag.GetTagByNameAndTenantIdAsync(name, tenantId));
    }
    
    // Get Tag by name and tenant id
    [HttpGet("{name}/tenant/{tenantId}")]
    public async Task<IActionResult> GetTagByNameAndTenantIdAsync(string name, int tenantId)
    {
        // Get the tag
        return Ok(await _tag.GetTagByNameAndTenantIdAsync(name, tenantId));
    }
    
    
    // Create the tag
    [HttpPost]
    public async Task<IActionResult> CreateTagAsync([FromBody] Tag tag)
    {
        var result = await _tag.CreateAsync(tag);
        if (string.IsNullOrWhiteSpace(result.Name)) return Ok(new Tag());
        return Ok(result);
    }
    
    // Get tag by name and location name
    [HttpGet("{name}/{locationName}")]
    public async Task<IActionResult> GetTagByNameAndLocationNameAsync(string name, string locationName)
    {
        // Get the tag
        return Ok(await _tag.GetTagByNameAndLocationNameAsync(name, locationName));
    }
    
    
}