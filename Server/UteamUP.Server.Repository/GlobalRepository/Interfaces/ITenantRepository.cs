namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ITenantRepository
{
    // Get all tenants which the user is member of
    Task<List<Tenant>> GetAllTenantsByOidAsync(string oid);
    // Create a new tenant
    Task<Tenant?> CreateTenantAsync(TenantDto tenant, string oid);
    Task<List<Tenant>> GetInvitesAsync(string oid);
    // Get the users owned tenants
    Task<List<Tenant>> GetOwnedTenantsAsync(string oid);
    Task<Tenant> GetTenantById(string tenantId);
}