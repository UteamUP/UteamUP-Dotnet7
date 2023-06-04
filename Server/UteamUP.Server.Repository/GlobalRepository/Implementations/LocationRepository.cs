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

    public async Task<Location?> CreateLocationAsync(Location? location, int tenantId)
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

    public async Task<Location?> UpdateLocationAsync(Location? location)
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

    public async Task<LocationDto?> GetByLocationId(int locationId)
    {
        var location = _context.Locations
            .Include(a => a.LocationTags)
            .ThenInclude(lt => lt.Tag)
            .FirstOrDefaultAsync(x => x.Id == locationId);
        
        // Map location to locationDto
        var mappedLocation = _mapper.Map<LocationDto>(location);
        return mappedLocation;
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
    
    public async Task<Location> UpdateLocationWithTags(Location location, List<Tag> tags)
    {
        var existingLocation = await _context.Locations.Include(l => l.LocationTags)
            .ThenInclude(lt => lt.Tag)
            .SingleOrDefaultAsync(l => l.Id == location.Id);

        if (existingLocation == null) throw new Exception("Location not found.");

        existingLocation.Name = location.Name;
        existingLocation.Description = location.Description;

        // Ensure the tags exist in the database, add them if they don't.
        location = await UpdateTagOnLocation(existingLocation, tags);

        // Get the list of tag IDs from the new list of tags
        var newTagIds = tags.Select(t => t.Id).ToList();

        // Remove the tags that are no longer linked with the location
        var tagsToRemove = existingLocation.LocationTags.Where(lt => !newTagIds.Contains(lt.TagId)).ToList();
        
        _context.LocationTags.RemoveRange(tagsToRemove);

        await _context.SaveChangesAsync();

        return existingLocation;
    }

    private async Task<Location> UpdateTagOnLocation(Location location, List<Tag> tags)
    {
        try{
            foreach (var tag in tags)
            { 
                tag.TenantId = location.TenantId;
                
                var existingTag = await _context.Tags.SingleOrDefaultAsync(t => t.Name == tag.Name);
                if (existingTag == null)
                {
                    tag.CreatedAt = DateTime.Now.ToUniversalTime();
                    tag.UpdatedAt = DateTime.Now.ToUniversalTime();
                    _context.Tags.Add(tag);
                }
                else
                {
                    tag.Id = existingTag.Id;
                }

                LocationTag locationTag = new LocationTag
                {
                    Location = location,
                    Tag = tag
                };
                
                location.LocationTags.Add(locationTag);
            }

            await _context.SaveChangesAsync();
            return location;
        }
        catch (Exception e)
        {
            return null;
        }
    }
    
    public async Task<Location?> CreateLocationWithTags(Location? location, List<Tag> tags)
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
        location = await _context
            .Locations
            .FirstOrDefaultAsync(x => x.Name == location.Name && x.TenantId == location.TenantId);

        // Update the tags on the location
        location = await UpdateTagOnLocation(location, tags);
        if(location == null)
            return null;
        
        // Return the location
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