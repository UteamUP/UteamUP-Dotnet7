namespace UteamUP.Client.Web.Repository.Interfaces;

public interface IPlanWebRepository
{
    Task<bool> CreatePlanAsync(PlanDto? plan);
    Task<List<Plan?>?> GetAllPlansAsync();
}