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
    private readonly IMapper _mapper;
    
    public LocationController(
        ILogger<LocationController> logger, 
        ILocationRepository location, 
        IMUserRepository user, IMapper mapper)
    {
        _logger = logger;
        _location = location;
        _user = user;
        _mapper = mapper;
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]LocationDto locationDto)
    {
        var user = await ValidateUser();
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;

        Location? location = new Location();
        List<Tag> tags = new List<Tag>();
        
        // Map LocationDto.Location to location using Mapper
        location = _mapper.Map<Location>(locationDto);
        
        // Map LocationDto.Tags to tags using Mapper
        tags = _mapper.Map<List<Tag>>(locationDto.Tags);
        
        // Create the location
        var result = await _location.CreateLocationWithTags(location, tags);

        if(result == null) return NotFound("Location not created");

        return Ok(result);
    }
    
    // Update
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] LocationDto? locationDto)
    {
        // Validate the user
        var user = await ValidateUser();
        
        // if unautorized return the error
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return user;
        
        Location? location = new Location();
        List<Tag> tags = new List<Tag>();
        
        // Map LocationDto.Location to location using Mapper
        location = _mapper.Map<Location>(locationDto);
        
        // Map LocationDto.Tags to tags using Mapper
        tags = _mapper.Map<List<Tag>>(locationDto.Tags);
        
        // Update the location
        var result = await _location.CreateLocationWithTags(location, tags);
        if(result == null) return NotFound("Location not updated");

        Console.WriteLine("The amount of tags: " + tags.Count);
        return Ok(result); // Return a success response
    }


    // Get location by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLocationByIdAsync(int id)
    {
        // Validate the user
        var user = await ValidateUser();
        
        // if unautorized return the error
        if (user.GetType() == typeof(UnauthorizedObjectResult))
            return new UnauthorizedResult();
        
        // Get the location
        var result = await _location.GetByLocationId(id);
        
        if(result == null) return NotFound("Location not found");
        
        return Ok(result);
    }
}