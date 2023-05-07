namespace UteamUP.Client.Web.Repository.Interfaces;

public interface IPlanRepository
{
    Task<bool> CreatePlanAsync(PlanDto? plan);
    Task<List<Plan?>?> GetAllPlansAsync();
}