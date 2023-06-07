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
    
    public async Task<List<Category>> CreateMultipleAsync(List<CategoryDto> categories, MUserDto? userDto, int tenantId)
    {
        _logger.Log(LogLevel.Information, $"{nameof(CreateMultipleAsync)}: Getting User for the categories");
        // Get the user and tenant
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Oid == userDto.Oid);
        
        _logger.Log(LogLevel.Information, $"{nameof(CreateMultipleAsync)}: Getting Tenant for the categories");
        // check if user exists
        if (string.IsNullOrWhiteSpace(user.Oid))
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateMultipleAsync)}: Could not find user");
            return new List<Category>();
        }
        
        // get the tenant
        var tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);
        // check if tenant exists
        if (string.IsNullOrWhiteSpace(tenant.Name))
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateMultipleAsync)}: Could not find tenant");
            return new List<Category>();
        }
        
        _logger.Log(LogLevel.Information, $"{nameof(CreateMultipleAsync)}: Mapping categories");
        // Create a range of categories
        var mycategories = _mapper.Map<List<Category>>(categories);
        
        _logger.Log(LogLevel.Information, $"{nameof(CreateMultipleAsync)}: Assigning tenant and user to the categories");
        // assign the tenant and user to the categories
        mycategories.ForEach(x => {
            x.Tenant = tenant;
            x.Creator = user;
            x.CreatedAt = DateTime.Now.ToUniversalTime();
            x.UpdatedAt = DateTime.Now.ToUniversalTime();
            x.TenantId = tenantId;
        });
        
        try{
            _logger.Log(LogLevel.Information, $"{nameof(CreateMultipleAsync)}: Adding categories to the database");
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

    public async Task<Category> CreateAsync(CategoryDto category, int tenantId, string oid)
    {
        try
        {
            // Check if the category already exists
            var exists = await _context.Categories.AnyAsync(x => x.Name == category.Name && x.TenantId == tenantId);
            
            // If the category already exists, return the category
            if (exists)
                return await _context.Categories.FirstOrDefaultAsync(x => x.Name == category.Name && x.TenantId == tenantId);
            
            // Get the tenant
            var tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);
            
            // Check if the tenant exists
            if (string.IsNullOrWhiteSpace(tenant.Name))
                return new Category();
            
            // Get the user
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Oid == oid);
            
            // Check if the user exists
            if (string.IsNullOrWhiteSpace(user.Oid))
                return new Category();
            
            // Map the category
            var mycategory = _mapper.Map<Category>(category);
            
            // Assign the tenant and user to the category
            mycategory.Tenant = tenant;
            mycategory.Creator = user;
            mycategory.CreatedAt = DateTime.Now.ToUniversalTime();
            mycategory.UpdatedAt = DateTime.Now.ToUniversalTime();
            
            // Add the category to the database
            _context.Add(mycategory);
            
            // Save the changes
            _context.SaveChanges();
            
            // Return the category
            return mycategory;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while creating the category");
            return new Category();
        }
        
        // Return the category
    }

    public async Task<Category> UpdateAsync(CategoryDto category, int id, int tenantId, string oid)
    {
        // Check if the category already exists
        var alreadyExists = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id && x.TenantId == tenantId);
            
        // If the category already exists, return the category
        if (alreadyExists == null)
            return new Category();
            
        // Get the tenant
        var tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);
            
        // Check if the tenant exists
        if (string.IsNullOrWhiteSpace(tenant.Name))
            return new Category();
            
        // Get the user
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Oid == oid);
            
        // Check if the user exists
        if (string.IsNullOrWhiteSpace(user.Oid))
            return new Category();
            
        // Update values
        alreadyExists.Name = category.Name;
        alreadyExists.UpdatedAt = DateTime.Now.ToUniversalTime();
        
        // Update the category
        _context.Update(alreadyExists);
        
        // Save the changes
        _context.SaveChanges();
        
        // Return the category
        return alreadyExists;
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

    public async Task<Category?> GetCategoryByIdAndTenantIdAsync(int id, int tenantId)
    {
        return await _context.Categories.Where(x => x.Id == id && x.TenantId == tenantId).FirstOrDefaultAsync();
    }
}