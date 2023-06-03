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

    public async Task<Location?> CreateLocationAsync(Location location, int tenantId)
    {
        var locationExists = await LocationExistsByNameAndTenantAsync(location.Name, tenantId);
        if (locationExists)
            return _context
                .Locations
                .Include(a => a.LocationTags)
                .ThenInclude(lt => lt.Tag)
                .FirstOrDefault(x => x.Name == location.Name && x.TenantId == tenantId);

        // Map the location
        //var mappedLocation = _mapper.Map<Location>(location);
        location.CreatedAt = DateTime.Now.ToUniversalTime();
        location.UpdatedAt = DateTime.Now.ToUniversalTime();
        
        // Add the location
        _context.Locations.Add(location);
        
        // Save the changes
        _context.SaveChanges();
        
        return location;
        
    }

    public async Task<Location?> UpdateLocationAsync(Location location)
    {
        location.UpdatedAt = DateTime.Now.ToUniversalTime();
        _context.Locations.Update(location);
        _context.SaveChanges();
        
        return location;
    }

    public async Task<Location?> UpdateTagToLocationAsync(int locationId, List<Tag> tags)
    {
        // Get the location based on the locationId
        var location = _context.Locations.FirstOrDefault(x => x.Id == locationId);
        
        // If the location is null return emtpy location
        if (location == null) return new Location();
        
        // Get the tags that are assigned to the location
        var locationTags = _context.LocationTags.Where(x => x.LocationId == locationId).ToList();
        
        // Remove tags that are not in the tags list
        _context.LocationTags.RemoveRange(locationTags.Where(x => !tags.Select(y => y.Id).Contains(x.TagId)));
        
        // Add tags that are not in the locationTags list
        _context.LocationTags.AddRange(tags.Where(x => !locationTags.Select(y => y.TagId).Contains(x.Id))
            .Select(x => new LocationTag
            {
                LocationId = locationId,
                TagId = x.Id
            }));
        
        // Save the changes
        _context.SaveChanges();
        
        return location;
    }

    public async Task<List<Location>> GetAllLocationsByTenantIdAsync(int tenantId)
    {
        return await _context
            .Locations
            .Include(a => a.LocationTags)
            .ThenInclude(lt => lt.Tag)
            .Where(x => x.TenantId == tenantId)
            .ToListAsync();
    }

    /*
    public async Task<List<Tag>> UpdateTagToLocationAsync(List<Tag> tags, int locationId)
    {
        // Select all tags that are in tags and locationId
        var locationTags = await _context.LocationTags.Where(x => x.LocationId == locationId).ToListAsync();

        // Remove all tags that are not in tags
        _context.LocationTags.RemoveRange(locationTags.Where(x => !tags.Select(y => y.Id).Contains(x.TagId)));
        
        // Add all tags that are not in locationTags
        _context.LocationTags.AddRange(tags.Where(x => !locationTags.Select(y => y.TagId).Contains(x.Id)).Select(x => new LocationTag
        {
            LocationId = locationId,
            TagId = x.Id
        }));

        // Save the changes
        _context.SaveChanges();
        
        return tags;
    }*/

    public Task<Location?> GetByLocationId(int locationId)
    {
        return _context.Locations
            .Include(a => a.LocationTags)
            .ThenInclude(lt => lt.Tag)
            .FirstOrDefaultAsync(x => x.Id == locationId);
    }

    /*
    public Task<List<Tag>> GetTagsByLocationId(int locationId)
    {
        return _context.LocationTags
            .Where(x => x.LocationId == locationId)
            .Select(x => x.Tag)
            .ToListAsync();
    }
    */
    
    // Check if the location already exists by name and tenant
    private async Task<bool> LocationExistsByNameAndTenantAsync(string name, int tenantId)
    {
        return await _context.Locations.AnyAsync(x => x.Name == name && x.TenantId == tenantId);
    }
    
    public async Task<Location> UpdateLocationWithTags(int locationId, List<Tag> newTags)
    {
        var location = await _context.Locations.Include(l => l.LocationTags).ThenInclude(lt => lt.Tag).FirstOrDefaultAsync(l => l.Id == locationId);
        location.LocationTags.Clear(); // This will remove all associations, but won't delete the tags
        foreach (var tag in newTags)
        {
            location.LocationTags.Add(new LocationTag { TagId = tag.Id });
        }
        await _context.SaveChangesAsync();
        return location;
    }
    
    public async Task<Location> CreateLocationWithTags(Location location, List<Tag> tags)
    {
        // Assuming _context is your DbContext
        foreach (var tag in tags)
        {
            location.LocationTags.Add(new LocationTag { Tag = tag });
        }
        _context.Locations.Add(location);
        await _context.SaveChangesAsync();
        return location;
    }
    
    public async Task DeleteLocation(int locationId)
    {
        var location = await _context.Locations.FindAsync(locationId);
        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();
    }

    public async Task<Location> GetLocationWithTags(int locationId)
    {
        return await _context.Locations.Include(l => l.LocationTags).ThenInclude(lt => lt.Tag).FirstOrDefaultAsync(l => l.Id == locationId);
    }


    
}