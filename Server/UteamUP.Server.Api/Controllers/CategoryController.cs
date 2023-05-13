using UteamUP.Server.Controllers;

namespace UteamUP.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ILogger<PlanController> _logger;
    private readonly ICategoryRepository _category;
    private readonly IMUserRepository _user;
    
    // Create the category
    [HttpPost("{id}")]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] List<CategoryDto> categories, int id)
    {
        _logger.Log(LogLevel.Information, $"{nameof(CreateCategoryAsync)}: Getting OID for user who is logged in");
        var oid = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? User.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
        if (oid == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateCategoryAsync)}: OID is empty");
            return BadRequest("OID is empty");
        }

        if (id == 0 || id == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateCategoryAsync)}: Id is empty");
            return BadRequest("Id is empty");
        }
        
        // Create the category
        _logger.Log(LogLevel.Information, $"{nameof(CreateCategoryAsync)}: Creating the categories");
        var result = await _category.CreateAsync(categories, oid, id);
        if (result == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateCategoryAsync)}: Something went wrong while creating the category");
            return BadRequest("Something went wrong while creating the category, please review logs for more information");
        }
        
        return Ok(result);
    }
    
    
}