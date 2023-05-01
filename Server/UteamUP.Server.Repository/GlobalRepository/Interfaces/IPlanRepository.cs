namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface IPlanRepository
{
    // Create plan
    Task<Plan> CreatePlanAsync(PlanDto plan);

    // Get plan by id
    Task<Plan> GetPlanByIdAsync(int id);

    // Get all plans
    Task<IEnumerable<Plan>> GetAllPlansAsync();

    // Update plan
    Task<Plan> UpdatePlanAsync(PlanDto plan);

    // Add plan discount
    Task<Plan> AddPlanDiscountAsync(float planDiscount);

    // Set plan discount expiry date
    Task<Plan> SetPlanDiscountExpiryDateAsync(DateTime planDiscountExpiryDate);
}