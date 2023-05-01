namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class PlanRepository : IPlanRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;

    public PlanRepository(pgContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Plan> CreatePlanAsync(PlanDto plan)
    {
        throw new NotImplementedException();
    }

    public async Task<Plan> GetPlanByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Plan>> GetAllPlansAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Plan> UpdatePlanAsync(PlanDto plan)
    {
        throw new NotImplementedException();
    }

    public async Task<Plan> AddPlanDiscountAsync(float planDiscount)
    {
        throw new NotImplementedException();
    }

    public async Task<Plan> SetPlanDiscountExpiryDateAsync(DateTime planDiscountExpiryDate)
    {
        throw new NotImplementedException();
    }
}