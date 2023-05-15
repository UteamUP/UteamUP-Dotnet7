namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<SubscriptionRepository> _logger;

    public SubscriptionRepository(
        pgContext context, 
        IMapper mapper, 
        ILogger<SubscriptionRepository> logger
        )
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }


    public async Task<Subscription> CreateAsync(int tenantId, int planId, int ExtraAmountOfLicenses)
    {
        try{
            // Check if the tenant exists
            var tenant = await _context.Tenants.FindAsync(tenantId);
            if (tenant == null)
            {
                _logger.Log(LogLevel.Warning, $"{nameof(CreateAsync)}: Tenant does not exist");
                return new Subscription();
            }
            
            // Check if the tenant already has a subscription
            var tenantHasSubscription = await _context.Subscriptions.AnyAsync(x => x.TenantId == tenantId);
            if (tenantHasSubscription)
            {
                _logger.Log(LogLevel.Warning, $"{nameof(CreateAsync)}: Tenant already has a subscription");
                return new Subscription();
            }
            
            // Check if the plan exists
            var plan = await _context.Plans.FindAsync(planId);
            if (plan == null)
            {
                _logger.Log(LogLevel.Warning, $"{nameof(CreateAsync)}: Plan does not exist");
                return new Subscription();
            }
            
            // Check if the plan is active
            if (!plan.IsActive)
            {
                _logger.Log(LogLevel.Warning, $"{nameof(CreateAsync)}: Plan is not active");
                return new Subscription();
            }
            
            // Create the subscription
            var subscription = new Subscription
            {
                TenantId = tenantId,
                PlanId = planId,
                ExtraAmountOfLicenses = ExtraAmountOfLicenses,
                IsActive = true,
                Guid = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now.ToUniversalTime(),
                UpdatedAt = DateTime.Now.ToUniversalTime()
            };
            
            // Add the subscription to the database
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
            
            // Return the subscription
            return subscription;
        }
        catch(Exception e){
            _logger.Log(LogLevel.Error, $"{nameof(CreateAsync)}: {e.Message}");
            return new Subscription();
        }
    }
}