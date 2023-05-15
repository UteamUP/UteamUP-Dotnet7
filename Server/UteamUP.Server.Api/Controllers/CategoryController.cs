namespace UteamUP.Server.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly ILogger<CategoryController> _logger;
    private readonly ICategoryRepository _category;
    private readonly IMUserRepository _user;
    
    public CategoryController(
        ILogger<CategoryController> logger, 
        ICategoryRepository category, 
        IMUserRepository user)
    {
        _logger = logger;
        _category = category;
        _user = user;
    }
    
    private async Task<MUserDto?> GetUser()
    {
        var oid = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? User.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
        Console.WriteLine("the users oid is : " + oid);
        if(string.IsNullOrWhiteSpace(oid))
            return new MUserDto();
        MUserDto? muser;
        try {
            muser = await _user.GetByOidDtoAsync(oid);
        } catch (Exception ex) {
            _logger.LogError(ex, $"{nameof(GetUser)}: Error retrieving user with oid {oid}");
            return new MUserDto(); // or handle it differently as per your use case
        }
        return muser;
    }
    
    // Get category by tenant id
    [HttpGet("tenant/{tenantId}")]
    public async Task<IActionResult> GetAllCategoriesByTenantIdAsync(int tenantId)
    {
        var result = await _category.GetAllCategoriesByTenantIdAsync(tenantId);
        if (result == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetAllCategoriesByTenantIdAsync)}: Something went wrong while retrieving the categories");
            return BadRequest("Something went wrong while retrieving the categories, please review logs for more information");
        }
        
        return Ok(result);
    }

    // Create the category
    [HttpPost("{id}")]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] List<CategoryDto> categories, int id)
    {
        var myuser = await GetUser();
        if (myuser.Oid == null)
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
        var result = await _category.CreateAsync(categories, myuser, id);
        if (result == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateCategoryAsync)}: Something went wrong while creating the category");
            return BadRequest("Something went wrong while creating the category, please review logs for more information");
        }
        return Ok(result);
    }
}