using Microsoft.Graph;
using UteamUP.Server.Repository.GenericRepository.Interfaces;
using Location = UteamUP.Shared.Models.Location;

namespace UteamUP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LocationController : ControllerBase
{
    private readonly ILogger<LocationController> _logger;
    private readonly ILocationRepository _location;
    private readonly IMUserRepository _user;
    private readonly IRepository<Location> _locationRepository;
    //private readonly IRepository<Tag> _tagRepository;
    
    public LocationController(
        ILogger<LocationController> logger, 
        ILocationRepository location, 
        IMUserRepository user, 
        IRepository<Location> locationRepository, 
        IRepository<Tag> tagRepository
        )
    {
        _logger = logger;
        _location = location;
        _user = user;
        //_locationRepository = locationRepository;
        //_tagRepository = tagRepository;
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
    
    // Create
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Location location)
    {
        // Validate the user
        var user = await ValidateUser();
        
        // if unautorized return the error
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;
        
        /*
        Location newLocation = new Location();
        _locationRepository.Add(newLocation);

        newLocation = location;
        _locationRepository.Update(newLocation);

        return Ok(newLocation); // Return a success response
        */
        
        return Ok(); // Return a success response*/
    }
    
    
    // Get all locations by tenant id
    [HttpGet("tenant/{tenantId}")]
    public async Task<IActionResult> GetLocationsByTenantIdAsync(int tenantId)
    {
        // Validate the user
        var user = await ValidateUser();
        
        // if unautorized return the error
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;
        
        // Get the locations
        var result = await _location.GetAllLocationsByTenantIdAsync(tenantId);
        if(result == null) return NotFound("Locations not found");
        
        return Ok(result);
    }
    
    // Create a location
    [HttpPost("tenant/{tenantId}")]
    public async Task<IActionResult> CreateLocationAsync(LocationDto location, int tenantId)
    {
        // Validate the user
        var user = await ValidateUser();
        
        // if unautorized return the error
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;
        
        // Create the location
        var result = await _location.CreateLocationAsync(location, tenantId);
        if(result == null) return NotFound("Location not created");
        
        return Ok(result);
    }
    
    // Get location by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLocationByIdAsync(int id)
    {
        // Validate the user
        var user = await ValidateUser();
        
        // if unautorized return the error
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;
        
        // Get the location
        var result = await _locationRepository.Get(id);
        if(result == null) return NotFound("Location not found");
        
        return Ok(result);
    }
    
}