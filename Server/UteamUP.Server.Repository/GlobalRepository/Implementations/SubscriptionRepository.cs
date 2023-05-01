namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;

    public SubscriptionRepository(pgContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Subscription> CreateSubscriptionAsync(SubscriptionDto subscription)
    {
        throw new NotImplementedException();
    }

    public async Task<Subscription> GetSubscriptionAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Subscription> AssignSubscriptionToTenantAsync(int id, int tenantId)
    {
        throw new NotImplementedException();
    }

    public async Task<Subscription> AssignPlanToSubscription(int id, int planId)
    {
        throw new NotImplementedException();
    }

    public async Task<Subscription> AddExtraLicensesToSubscriptionAsync(int id, int extraLicenses)
    {
        throw new NotImplementedException();
    }

    public async Task<Subscription> RemoveExtraLicensesFromSubscriptionAsync(int id, int extraLicenses)
    {
        throw new NotImplementedException();
    }

    public async Task<Subscription> CalculateSubscriptionPriceAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Subscription> CalculateSubscriptionLicensesUsedAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Subscription> CalculateSubscriptionLicensesAvailableAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Subscription> UpgradeSubscriptionAsync(int id, int planId)
    {
        throw new NotImplementedException();
    }

    public async Task<Subscription> DowngradeSubscriptionAsync(int id, int planId)
    {
        throw new NotImplementedException();
    }

    public async Task<Subscription> CancelSubscriptionAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Subscription> ReactivateSubscriptionAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Subscription> CalculateNextSubscriptionBillingDateAsync(int id)
    {
        throw new NotImplementedException();
    }
}