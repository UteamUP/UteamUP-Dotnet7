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
            return _context.Locations.FirstOrDefault(x => x.Name == location.Name && x.TenantId == tenantId);

        Console.WriteLine("Trying to create location");
        
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

    public async Task<List<Location>> GetAllLocationsByTenantIdAsync(int tenantId)
    {
        return await _context.Locations.Where(x => x.TenantId == tenantId).ToListAsync();
    }

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
    }

    public Task<Location> GetByLocationId(int locationId)
    {
        return _context.Locations
            .FirstOrDefaultAsync(x => x.Id == locationId);
    }

    public Task<List<Tag>> GetTagsByLocationId(int locationId)
    {
        return _context.LocationTags
            .Where(x => x.LocationId == locationId)
            .Select(x => x.Tag)
            .ToListAsync();
    }

    // Check if the location already exists by name and tenant
    private async Task<bool> LocationExistsByNameAndTenantAsync(string name, int tenantId)
    {
        return await _context.Locations.AnyAsync(x => x.Name == name && x.TenantId == tenantId);
    }
}