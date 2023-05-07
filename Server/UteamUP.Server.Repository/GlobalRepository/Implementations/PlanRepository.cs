namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class PlanRepository : IPlanRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<PlanRepository> _logger;
    
    public PlanRepository(pgContext context, IMapper mapper, ILogger<PlanRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Plan> CreatePlanAsync(PlanDto plan)
    {
        // Check if the plan already exists
        var planExists = PlanExistsByNameAsync(plan.Name);
        if (planExists.Result)
        {
            _logger.Log(LogLevel.Warning, "CreatePlanAsync: Plan already exists by name");
            return new Plan();
        }
        
        // Check if the plan already exists by sku
        var planExistsBySku = PlanExistsBySkuAsync(plan.Sku);
        if (planExistsBySku.Result)
        {
            _logger.Log(LogLevel.Warning, "CreatePlanAsync: Plan already exists by sku");
            return new Plan();
        }
        
        try{
            // Map the plan
            var mappedPlan = _mapper.Map<Plan>(plan);

            mappedPlan.CreatedAt = DateTime.Now.ToUniversalTime();
            mappedPlan.UpdatedAt = DateTime.Now.ToUniversalTime();
            
            // Add the plan
            _context.Plans.Add(mappedPlan);
            
            // Save the changes
            _context.SaveChanges();
            
            // Return the plan
            return mappedPlan;
        }
        catch(Exception e)
        {
            _logger.Log(LogLevel.Error, "CreatePlanAsync: " + e.Message);
            return new Plan();
        }
    }

    public async Task<List<Plan>> GetAllPlansAsync()
    {
        // Get all plans
        return await  _context.Plans.ToListAsync();
    }

    // Check if the plan already exists
    private async Task<bool> PlanExistsAsync(int id)
    {
        return await _context.Plans.AnyAsync(x => x.Id == id);
    }
    
    // Check if the plan already exists by name
    private async Task<bool> PlanExistsByNameAsync(string name)
    {
        return await _context.Plans.AnyAsync(x => x.Name == name);
    }
    
    // Check if the plan already exists by sku
    private async Task<bool> PlanExistsBySkuAsync(string sku)
    {
        return await _context.Plans.AnyAsync(x => x.Sku == sku);
    }
}