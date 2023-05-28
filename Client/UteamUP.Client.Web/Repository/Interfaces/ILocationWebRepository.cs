namespace UteamUP.Client.Web.Repository.Interfaces;

public interface ILocationWebRepository
{
    Task<List<Location>> GetAllLocationsByTenantIdAsync(int tenantId);
    Task<Location?> CreateLocationAsync(LocationDto location, int tenantId);
}