namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ISubscriptionRepository
{
    // Create a subscription
    Task<Subscription> CreateAsync(int tenantId, int planId, int ExtraAmountOfLicenses);
    Task<Subscription> GetByTenantIdAsync(int tenantId);
}