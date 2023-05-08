namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ITenantRepository
{
    // Get all tenants which the user is member of
    Task<List<Tenant>> GetAllTenantsAsyncByOid(string oid);

    // List all invites of the tenant
    //Task<List<InvitedUser>> GetTenantInvitesAsync(string email);
    
    // Create a new tenant
    Task<Tenant?> CreateTenantAsync(TenantDto tenant, string oid);

    Task<List<Tenant>> GetInvitesAsync(string oid);
    /*
    // Get all tenants which the user is owner of the tenant
    Task<List<Tenant>> GetAllOwnedTenantsAsync(string oid);

    // Get users default tenant
    Task<Tenant> GetDefaultTenantAsync(string oid);

    // Get tenant by id
    Task<Tenant> GetTenantAsync(int id);

    // Assign tenant to the user
    Task<Tenant> AssignTenantAsync(string oid, int tenantId);

    // Remove tenant from the user
    Task<Tenant> RemoveTenantAsync(string oid, int tenantId);

    // Set default tenant for the user
    Task<Tenant> SetDefaultTenantAsync(string oid, int tenantId);

    // Get all members of the specified tenant
    Task<List<MUserDto>> GetTenantMembersAsync(int tenantId);

    // Change the owner of the tenant
    Task<Tenant> ChangeTenantOwnerAsync(int tenantId, string newOwnerOid);

    // Create a new tenant
    Task<Tenant> CreateTenantAsync(TenantDto tenant);

    // Update the tenant
    Task<Tenant> UpdateTenantAsync(TenantDto tenant);

    // Disable the tenant
    Task<bool> DisableTenantAsync(int tenantId);

    // Enable the tenant
    Task<bool> EnableTenantAsync(int tenantId);

    // Create user invite to tenant
    Task<InvitedUser> CreateTenantInviteAsync(string email, int tenantId);

    // Remove user invite from tenant
    Task<bool> RemoveTenantInviteAsync(int inviteId);

    // Accept invite to tenant
    Task<InvitedUser> AcceptTenantInviteAsync(int inviteId, string oid);

    // Remove member from tenant
    Task<bool> RemoveTenantMemberAsync(int memberId, int tenantId);
    */
}