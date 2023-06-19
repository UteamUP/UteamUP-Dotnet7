namespace UteamUP.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StockController : ControllerBase
{
    private readonly ILogger<StockController> _logger;
    private readonly IStockRepository _category;
    private readonly IMUserRepository _user;
    private readonly IMapper _mapper;

    public StockController(
        IStockRepository category, 
        ILogger<StockController> logger, 
        IMUserRepository user
        )
    {
        _category = category;
        _logger = logger;
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
    
    // Post new stock
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] StockDto stockDto)
    {
        var user = await ValidateUser();
        if(user is UnauthorizedResult)
            return user;
        
        // Check if stockItems.Stock is null
        if(string.IsNullOrWhiteSpace(stockDto.Name))
            return BadRequest("Stock cannot be null");

        var result = await _category.CreateStockWithTags(stockDto);
        if(result == null) return NotFound("Stock not created");
        
        return Ok(result);
    }
    
    // Update stock
    [HttpPut("{stockId}")]
    public async Task<IActionResult> Put(int stockId, [FromBody] StockDto stockItem)
    {
        var user = await ValidateUser();
        if(user is UnauthorizedResult)
            return user;

        // Check if stockItems.Stock is null
        if(string.IsNullOrWhiteSpace(stockItem.Name))
            return BadRequest("Stock cannot be null");
        
        // Check if stockId is 0
        if(stockId == 0)
            return BadRequest("StockId cannot be 0.");
        
        var stock = await _category.UpdateStockWithTags(stockItem, stockId);
        return Ok(stock);
    }
    
    // Get stock by tenant id
    [HttpGet("tenant/{tenantId}")]
    public async Task<IActionResult> Get(int tenantId)
    {
        var user = await ValidateUser();
        if(user is UnauthorizedResult)
            return user;
        
        if(tenantId == 0)
            return BadRequest("TenantId cannot be 0.");
        
        var stock = await _category.GetStockByTenantId(tenantId);
        return Ok(stock);
    }
    
    // Get stock by stock id
    [HttpGet("{stockId}/tenant/{tenantId}")]
    public async Task<IActionResult> GetByStockId(int stockId, int tenantId)
    {
        var user = await ValidateUser();
        if(user is UnauthorizedResult)
            return user;

        if (stockId == 0)
            return BadRequest("StockId cannot be 0.");
        
        if (tenantId == 0)
            return BadRequest("TenantId cannot be 0.");
        
        var stock = await _category.GetByStockId(stockId, tenantId);
        return Ok(stock);
    }
    
}