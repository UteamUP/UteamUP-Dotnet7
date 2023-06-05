using System.Text.Json;

namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class LocationRepository : ILocationRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<LocationRepository> _logger;
    
    public LocationRepository(
        IMapper mapper, 
        pgContext context, 
        ILogger<LocationRepository> logger
        )
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }

    public async Task<LocationTagDto?> GetByLocationId(int locationId)
    {
        LocationTagDto locations = await _context.Locations
            .Include(l => l.LocationTags)
            .ThenInclude(lt => lt.Tag)
            .Where(l => l.Id == locationId)
            .Select(l => new LocationTagDto
            {
                Name = l.Name,
                Description = l.Description,
                TenantId = l.TenantId,
                Tags = l.LocationTags.Select(t => new TagDto
                {
                    TenantId = t.Tag.TenantId,
                    Name = t.Tag.Name
                }).ToList()
            })
            .FirstOrDefaultAsync();
        
        return locations;
    }

    // Check if the location already exists by name and tenant
    private async Task<bool> LocationExistsByNameAndTenantAsync(string name, int tenantId)
    {
        return await _context.Locations.AnyAsync(x => x.Name == name && x.TenantId == tenantId);
    }
    
    public async Task<Location> UpdateLocationWithTags(Location location, List<string> tags, int id)
    {
        // Search for the location in the database, do not include tags
        var existingLocation = await _context.Locations
            .Include(l => l.LocationTags)
            .ThenInclude(lt => lt.Tag)
            .FirstOrDefaultAsync(l => l.Id == id && l.TenantId == location.TenantId);
        
        // If the location is null return emtpy location
        if (existingLocation == null) throw new Exception("Location not found.");
        
        // Update the location
        existingLocation.Name = location.Name;
        existingLocation.Description = location.Description;
        existingLocation.UpdatedAt = DateTime.Now.ToUniversalTime();
        
        // Update the location
        _context.Locations.Update(existingLocation);
        
        // Save the changes
        await _context.SaveChangesAsync();

        // Compare location and tags and add new tags that are not in existingLocation
        foreach (var tag in tags)
        {
            // Check if the tag does not exists in the database then add it
            if(!_context.Tags.Any(t => t.Name == tag && t.TenantId == location.TenantId)) // Fix is here
            {
                Tag newTag = new();
                newTag.Name = tag;
                newTag.TenantId = location.TenantId;
                newTag.CreatedAt = DateTime.Now.ToUniversalTime();
                newTag.UpdatedAt = DateTime.Now.ToUniversalTime();
                await _context.Tags.AddAsync(newTag);
                await _context.SaveChangesAsync();
            }
        
            // Check if the tag does not exists in the location then add it
            if(!existingLocation.LocationTags.Any(t => t.Tag.Name == tag))
            {
                Tag newTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tag && t.TenantId == location.TenantId);
                LocationTag locationTag = new();
                locationTag.Location = existingLocation;
                locationTag.Tag = newTag;
            
                await _context.LocationTags.AddAsync(locationTag);
                await _context.SaveChangesAsync();
            }
        }
        
        // Get existing location with tags again to see if the tags were updated
        existingLocation = await _context.Locations
            .Include(l => l.LocationTags)
            .ThenInclude(lt => lt.Tag)
            .FirstOrDefaultAsync(l => l.Id == id && l.TenantId == location.TenantId);
        
        // Compare tags and location and remove tags in existingLocation that are not in tags
        foreach (var locationTag in existingLocation.LocationTags)
        {
            // Check if the tag does not exists in the location then add it
            if(!tags.Any(t => t == locationTag.Tag.Name))
            {
                _context.LocationTags.Remove(locationTag);
                await _context.SaveChangesAsync();
            }
        }

        return existingLocation;
    }

    public async Task<Location?> CreateLocationWithTags(Location? location, List<string> tags)
    {
        // Check if the location already exists
        var locationExists = await LocationExistsByNameAndTenantAsync(location.Name, location.TenantId);
        if (locationExists)
            return _context
                .Locations
                .Include(a => a.LocationTags)
                .ThenInclude(lt => lt.Tag)
                .FirstOrDefault(x => x.Name == location.Name && x.TenantId == location.TenantId);

        // set the created at and updated at on location
        location.CreatedAt = DateTime.Now.ToUniversalTime();
        location.UpdatedAt = DateTime.Now.ToUniversalTime();
        _context.Locations.Add(location);
        
        await _context.SaveChangesAsync();
        
        // Get the location
        var existingLocation = await _context.Locations
            .Include(l => l.LocationTags)
            .ThenInclude(lt => lt.Tag)
            .FirstOrDefaultAsync(l => l.Id == location.Id && l.TenantId == location.TenantId);

        foreach (var tag in tags)
        {
            // Check if the tag does not exists in the database then add it
            if(!_context.Tags.Any(t => t.Name == tag && t.TenantId == location.TenantId)) // Fix is here
            {
                Tag newTag = new();
                newTag.Name = tag;
                newTag.TenantId = location.TenantId;
                newTag.CreatedAt = DateTime.Now.ToUniversalTime();
                newTag.UpdatedAt = DateTime.Now.ToUniversalTime();
                await _context.Tags.AddAsync(newTag);
                await _context.SaveChangesAsync();
            }
        
            // Check if the tag does not exists in the location then add it
            if(!existingLocation.LocationTags.Any(t => t.Tag.Name == tag))
            {
                Tag newTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tag && t.TenantId == location.TenantId);
                LocationTag locationTag = new();
                locationTag.Location = existingLocation;
                locationTag.Tag = newTag;
            
                await _context.LocationTags.AddAsync(locationTag);
                await _context.SaveChangesAsync();
            }
        }
        
        // Return the location
        return existingLocation;
    }
}