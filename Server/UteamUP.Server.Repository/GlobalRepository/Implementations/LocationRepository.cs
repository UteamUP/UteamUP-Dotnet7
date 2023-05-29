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

    public async Task<Location?> CreateLocationAsync(LocationDto location, int tenantId)
    {
        var locationExists = await LocationExistsByNameAndTenantAsync(location.Name, tenantId);
        if (locationExists)
            return _context.Locations.FirstOrDefault(x => x.Name == location.Name);

        Console.WriteLine("Trying to create location");
        
        // Map the location
        var mappedLocation = _mapper.Map<Location>(location);
        mappedLocation.CreatedAt = DateTime.Now.ToUniversalTime();
        mappedLocation.UpdatedAt = DateTime.Now.ToUniversalTime();
        
        // Add the location
        _context.Locations.Add(mappedLocation);
        
        // Save the changes
        _context.SaveChanges();
        
        return mappedLocation;
        
    }

    public async Task<List<Location>> GetAllLocationsByTenantIdAsync(int tenantId)
    {
        return await _context.Locations.Where(x => x.TenantId == tenantId).ToListAsync();
    }
    
    // Check if the location already exists by name and tenant
    private async Task<bool> LocationExistsByNameAndTenantAsync(string name, int tenantId)
    {
        return await _context.Locations.AnyAsync(x => x.Name == name && x.TenantId == tenantId);
    }
}