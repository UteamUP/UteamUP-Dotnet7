using UteamUP.Server.Api.Helpers;

namespace UteamUP.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserProfileController : ControllerBase
{
    private readonly ILogger<UserProfileController> _logger;
    private readonly IMapper _mapper;
    private readonly IProfileBuilder _profileBuilder;
    public UserProfileController(
        IProfileBuilder profileBuilder,
        ILogger<UserProfileController> logger, IMapper mapper)
    {
        _profileBuilder = profileBuilder;
        _logger = logger;
        _mapper = mapper;
    }
    
    private string GetOid()
    {
        return User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? User.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        // Get the oid from the user who is logged in
        var oid = GetOid();
        
        // if the oid is null or empty, return unauthorized
        if(string.IsNullOrWhiteSpace(oid))
            return Unauthorized("You are not authorized to perform this action");
        
        // Get the user from the database
        var user = await _profileBuilder.GetUserProfile(oid);
        
        return Ok(user);
    }
}