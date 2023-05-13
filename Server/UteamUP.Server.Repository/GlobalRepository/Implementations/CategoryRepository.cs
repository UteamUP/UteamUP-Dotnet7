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
    
    public async Task<List<Category>> CreateAsync(List<CategoryDto> categories, string oid, int tenantId)
    {
        // Get the user and tenant
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Oid == oid);
        // check if user exists
        if(user == null) return new List<Category>();
        // get the tenant
        var tenant = await _context.Tenants.FirstOrDefaultAsync(x => x.Id == tenantId);
        // check if tenant exists
        if(tenant == null) return new List<Category>();
        
        // Create a range of categories
        var mycategories = _mapper.Map<List<Category>>(categories);
        
        // assign the tenant and user to the categories
        mycategories.ForEach(x => {
            x.Tenant = tenant;
            x.Creator = user;
        });
        
        try{
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
}