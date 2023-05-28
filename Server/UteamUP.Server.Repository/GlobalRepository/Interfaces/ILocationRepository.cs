namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ILocationRepository
{
    Task<Location?> CreateLocationAsync(LocationDto location, int tenantId);

    Task<List<Location>> GetAllLocationsByTenantIdAsync(int tenantId);
}