namespace UteamUP.Client.Web.Repository.Interfaces;

public interface ISubscriptionWebRepository
{
    Task<Subscription> GetSubscriptionByTenantId(int tenantId);
}