using System.Security.Claims;

namespace UteamUP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PlanController : ControllerBase
{
    private readonly ILogger<PlanController> _logger;
    private readonly IPlanRepository _plan;
    private readonly IMUserRepository _user;
    
    public PlanController(
        IPlanRepository plan, 
        ILogger<PlanController> logger, 
        IMUserRepository user)
    {
        _plan = plan;
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
        
        // Validate that the user is admin
        var muser = await _user.GetByOidAsync(oid);
        Console.WriteLine("The user with the oid: " + oid + " is admin: " + muser.IsAdmin);
        if(muser.IsAdmin == false) return Unauthorized("You are not authorized to perform this action");

        return Ok(true);
    }
    
    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> GetAllPlansAsync()
    {
        var result = await _plan.GetAllPlansAsync();
        if(result == null) return BadRequest("Something went wrong while getting the plans, please review logs for more information");
        
        return Ok(result);
    }
    
    // Create the plan
    [HttpPost]
    public async Task<IActionResult> CreatePlanAsync([FromBody] PlanDto plan)
    {
        // Validate the user
        var user = await ValidateUser();
        // if unautorized return the error
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;   
        
        var result = await _plan.CreatePlanAsync(plan);
        if(string.IsNullOrWhiteSpace(result.Name)) return BadRequest("Something went wrong while creating the plan, please review logs for more information");
        
        return Ok(result);
    }
}