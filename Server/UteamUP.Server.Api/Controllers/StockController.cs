namespace UteamUP.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class StockController : ControllerBase
{
    private readonly ILogger<StockController> _logger;
    private readonly IStockRepository _category;
    private readonly IMUserRepository _user;
    
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
    public async Task<IActionResult> Post([FromBody] StockTagDto stockItems)
    {
        var user = await ValidateUser();
        if(user is UnauthorizedResult)
            return user;
        
        var stock = await _category.CreateStockWithTags(stockItems);
        return Ok(stock);
    }
    
    // Update stock
    [HttpPut("{stockId}")]
    public async Task<IActionResult> Put(int stockId, [FromBody] StockTagDto stockItems)
    {
        var user = await ValidateUser();
        if(user is UnauthorizedResult)
            return user;
        
        var stock = await _category.UpdateStockWithTags(stockItems, stockId);
        return Ok(stock);
    }
    
    // Get stock by tenant id
    [HttpGet("tenant/{tenantId}")]
    public async Task<IActionResult> Get(int tenantId)
    {
        var user = await ValidateUser();
        if(user is UnauthorizedResult)
            return user;
        
        var stock = await _category.GetStockByTenantId(tenantId);
        return Ok(stock);
    }
    
    // Get stock by stock id
    [HttpGet("{stockId}")]
    public async Task<IActionResult> GetByStockId(int stockId)
    {
        var user = await ValidateUser();
        if(user is UnauthorizedResult)
            return user;
        
        var stock = await _category.GetByStockId(stockId);
        return Ok(stock);
    }
    
}