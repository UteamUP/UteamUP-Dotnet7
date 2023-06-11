namespace UteamUP.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PartController : ControllerBase
{
    private readonly ILogger<PartController> _logger;
    private readonly IPartRepository _part;
    private readonly IMUserRepository _user;

    public PartController(IPartRepository part, ILogger<PartController> logger, IMUserRepository user)
    {
        _part = part;
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
    
    // Create the part
    [HttpPost("{tenantId}")]
    public async Task<IActionResult> CreatePartAsync([FromBody] PartDto part, int tenantId)
    {
        // Validate the user
        var user = await ValidateUser();
        
        // if unautorized return the error
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;

        var result = await _part.CreatePartAsync(part, tenantId);
        if (result == null) return BadRequest("Something went wrong while creating the part, please review logs for more information");

        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}