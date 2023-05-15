namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class CategoryRepository : ICategoryRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CategoryRepository> _logger;
    
    public CategoryRepository(pgContext context, IMapper mapper, ILogger<CategoryRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<List<Category>> CreateAsync(List<CategoryDto> categories, MUserDto? userDto, int tenantId)
    {
        _logger.Log(LogLevel.Information, $"{nameof(CreateAsync)}: Getting User for the categories");
        // Get the user and tenant
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Oid == userDto.Oid);
        
        _logger.Log(LogLevel.Information, $"{nameof(CreateAsync)}: Getting Tenant for the categories");
        // check if user exists
        if (string.IsNullOrWhiteSpace(user.Oid))
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateAsync)}: Could not find user");
            return new List<Category>();
        }
        
        // get the tenant
        var tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);
        // check if tenant exists
        if (string.IsNullOrWhiteSpace(tenant.Name))
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateAsync)}: Could not find tenant");
            return new List<Category>();
        }
        
        _logger.Log(LogLevel.Information, $"{nameof(CreateAsync)}: Mapping categories");
        // Create a range of categories
        var mycategories = _mapper.Map<List<Category>>(categories);
        
        _logger.Log(LogLevel.Information, $"{nameof(CreateAsync)}: Assigning tenant and user to the categories");
        // assign the tenant and user to the categories
        mycategories.ForEach(x => {
            x.Tenant = tenant;
            x.Creator = user;
            x.CreatedAt = DateTime.Now.ToUniversalTime();
            x.UpdatedAt = DateTime.Now.ToUniversalTime();
            x.TenantId = tenantId;
        });
        
        try{
            _logger.Log(LogLevel.Information, $"{nameof(CreateAsync)}: Adding categories to the database");
            // Add the categories to the database
            _context.AddRange(mycategories);
            
            // Save the changes
            _context.SaveChanges();
            
            // Return the categories
            return mycategories;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while creating the category");
            return new List<Category>();
        }
    }

    public async Task<List<Category>> GetAllCategoriesByTenantIdAsync(int tenantId)
    {
        try
        {
            _logger.Log(LogLevel.Information, $"{nameof(GetAllCategoriesByTenantIdAsync)}: Getting all categories by tenant id");
            // Get all categories by tenant id
            var categories = await _context.Categories.Where(x => x.TenantId == tenantId).ToListAsync();
            
            // Return the categories
            return categories;
        }catch(Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while retrieving the categories");
            return new List<Category>();
        }
    }
}