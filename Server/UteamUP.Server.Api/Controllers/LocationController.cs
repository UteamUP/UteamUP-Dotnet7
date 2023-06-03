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
    private readonly IRepository<Tag> _tagRepository;
    
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
        _locationRepository = locationRepository;
        _tagRepository = tagRepository;
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

    [HttpPost("add")]
    public async Task<IActionResult> Create([FromBody] LocationDto location)
    {
        var user = await ValidateUser();
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;

        return Ok(new Location()); 
    }
    
    // Update
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Location location)
    {
        // Validate the user
        var user = await ValidateUser();
        
        // if unautorized return the error
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;
        
        // Update the location
        var result = await _location.UpdateLocationAsync(location);
        if(result == null) return NotFound("Location not updated");
        
        // Update the tags
        //var tagUpdate = await _location.UpdateTagToLocationAsync(location.Tags, location.Id);
        //if(tagUpdate == null) return NotFound("Tags not updated");
        
        return Ok(result); // Return a success response
    }
    
    /*
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
    */
    
    // Create a location
    /*[HttpPost("tenant/{tenantId}")]
    public async Task<IActionResult> CreateLocationAsync(Location location, int tenantId)
    {
        // Validate the user
        var user = await ValidateUser();
        
        // if unautorized return the error
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;
        
        // Create the location
        var result = await _location.CreateLocationAsync(location, tenantId);
        if(result == null) return NotFound("Location not created");
        
        //((var tagUpdate = await _location.UpdateTagToLocationAsync(location.Tags, result.Id);
        //if(tagUpdate == null) return NotFound("Tags not updated");
        
        return Ok(result);
    }*/

    /*
    // Assign tags to location
    [HttpPost("{id}/tags")]
    public async Task<IActionResult> AssignTagsToLocationAsync([FromBody] List<Tag> tags, int id)
    {
        // Validate the user
        var user = await ValidateUser();
        
        // if unautorized return the error
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;
        
        // Update the tags
        var tagUpdate = await _location.UpdateTagToLocationAsync(id, tags);
        if(tagUpdate == null) return NotFound("Tags not updated");
        
        return Ok(tagUpdate);
    }
    */
    
    /*[HttpGet("{id}/tags")]
    public async Task<IActionResult> GetTagsByLocationId(int id)
    {
        var user = await ValidateUser();
        
        // if unautorized return the error
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;
        
        var result = await _location.GetByLocationId(id);
        if(result == null) return NotFound("Location not found");
        
        var tags = result.LocationTags.Select(lt => lt.Tag).ToList();
        if(tags == null) return NotFound("Tags not found");
        
        return Ok(tags);
    }*/
    
    
    // Get location by id
    /*[HttpGet("{id}")]
    public async Task<IActionResult> GetLocationByIdAsync(int id)
    {
        // Validate the user
        var user = await ValidateUser();
        
        // if unautorized return the error
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;
        
        // Get the location
        //var result = await _locationRepository.Get(id);
        var result = await _location.GetByLocationId(id);
        
        if(result == null) return NotFound("Location not found");
        
        return Ok(result);
    }*/
    
}