namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class VendorRepository : IVendorRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<VendorRepository> _logger;
    
    public VendorRepository(pgContext context, IMapper mapper, ILogger<VendorRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }


    public async Task<Vendor?> CreateAsync(VendorDto vendor, string oid)
    {
        // Check if vendor is null
        if (string.IsNullOrWhiteSpace(vendor.Name))
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateAsync)}: Vendor is null");
            return new Vendor();
        }
        
        // Check if vendor already exists
        if (await ExistsByNameAsync(vendor.Name))
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateAsync)}: Vendor already exists");
            return new Vendor();
        }

        // Check if user is null
        if (string.IsNullOrWhiteSpace(oid))
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateAsync)}: Oid is null");
            return new Vendor();
        }
        
        // Get user by oid
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Oid == oid);
        
        // Check if user is null
        if (string.IsNullOrWhiteSpace(user?.Oid))
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateAsync)}: User is null");
            return new Vendor();
        }
        
        // Map vendordto to vendor
        var mappedVendor = _mapper.Map<Vendor>(vendor);
        
        try
        {
            // Assign user to vendor
            mappedVendor.Creator = user;
            
            // Update created and updated at
            mappedVendor.CreatedAt = DateTime.Now.ToUniversalTime();
            mappedVendor.UpdatedAt = DateTime.Now.ToUniversalTime();
            
            // Add vendor to database
            await _context.Vendor.AddAsync(mappedVendor);
            await _context.SaveChangesAsync();
            
            return mappedVendor;
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateAsync)}: {e.Message}");
            return new Vendor();
        }
    }

    public async Task<List<Vendor>> GetAllAsync()
    {
        return await _context.Vendor.ToListAsync();
    }

    // Check if vendor already exists by name
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Vendor.AnyAsync(x => x.Name == name);
    }
}