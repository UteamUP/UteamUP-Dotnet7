namespace UteamUP.Client.Web.Repository.Interfaces;

public interface ILocationWebRepository
{
    Task<List<Location>> GetAllLocationsByTenantIdAsync(int tenantId);
    Task<Location> GetByLocationId(int locationId);
    Task<Location?> Create(Location location);
    Task<List<Tag>> GetTagsByLocationId(int locationId);
}