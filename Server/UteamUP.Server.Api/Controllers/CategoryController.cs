using Microsoft.AspNetCore.Http.HttpResults;

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
    
    private async Task<IActionResult> ValidateUser()
    {
        // Get the oid from the user who is logged in
        var oid = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? User.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
        // Get email from the user who is logged in
        var email = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/signInNames.emailAddress")?.Value ?? User.Claims.FirstOrDefault(c => c.Type == "signInNames.emailAddress")?.Value;
        
        if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(oid))
            return Unauthorized("You are not authorized to perform this action");
        
        return Ok(true);
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

    // Get category by id and tenant id
    [HttpGet("{id}/tenant/{tenantId}")]
    public async Task<IActionResult> GetCategoryByIdAsync(int id, int tenantId)
    {
        var result = await _category.GetCategoryByIdAndTenantIdAsync(id, tenantId);
        if (result == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetCategoryByIdAsync)}: Something went wrong while retrieving the category");
            return BadRequest("Something went wrong while retrieving the category, please review logs for more information");
        }
        return Ok(result);
    }
    
    // Create multiple categories
    [HttpPost("add/tenant/{tenantId}/multiple")]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] List<CategoryDto> categories, int tenantId)
    {
        var myuser = await GetUser();
        if (myuser.Oid == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateCategoryAsync)}: OID is empty");
            return BadRequest("OID is empty");
        }
        if (tenantId <= 0 || tenantId == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateCategoryAsync)}: Id is empty");
            return BadRequest("Id is empty");
        }
        // Create the category
        _logger.Log(LogLevel.Information, $"{nameof(CreateCategoryAsync)}: Creating the categories");
        var result = await _category.CreateMultipleAsync(categories, myuser, tenantId);
        if (result == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateCategoryAsync)}: Something went wrong while creating the category");
            return BadRequest("Something went wrong while creating the category, please review logs for more information");
        }
        return Ok(result);
    }
    
    // Create category
    [HttpPost("tenant/{tenantId}/oid/{oid}")]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryDto? category, int tenantId, string oid)
    {
        var user = await ValidateUser();
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;

        if (category == null)
            return NoContent();

        if (tenantId <= 0 || tenantId == null)
            return BadRequest();

        var result = await _category.CreateAsync(category, tenantId, oid);
        Console.WriteLine("The category was created successfully : " + result.Name);
        if (result == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateCategoryAsync)}: Something went wrong while creating the category");
            return BadRequest("Something went wrong while creating the category, please review logs for more information");
        }
        
        return Ok(result);
    }
    
    [HttpPut("{id}/tenant/{tenantId}/oid/{oid}")]
    public async Task<IActionResult> UpdateCategoryAsync([FromBody] CategoryDto? category, int id, int tenantId, string oid)
    {
        var user = await ValidateUser();
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;

        if (category == null)
            return NoContent();

        if (tenantId <= 0 || tenantId == null)
            return BadRequest();

        var result = await _category.UpdateAsync(category, id, tenantId, oid);
        
        if (result == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(UpdateCategoryAsync)}: Something went wrong while updating the category");
            return BadRequest("Something went wrong while updating the category, please review logs for more information");
        }
        
        return Ok(result);
    }

    
}