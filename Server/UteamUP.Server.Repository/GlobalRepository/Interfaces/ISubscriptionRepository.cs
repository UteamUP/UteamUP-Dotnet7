namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ISubscriptionRepository
{
    // Create a subscription
    Task<Subscription> CreateSubscriptionAsync(SubscriptionDto subscription);

    // Get a subscription by id
    Task<Subscription> GetSubscriptionAsync(int id);

    // Assign tenant to subscription
    Task<Subscription> AssignSubscriptionToTenantAsync(int id, int tenantId);

    // Assign plan to subscription
    Task<Subscription> AssignPlanToSubscription(int id, int planId);

    // Add extra licenses to subscription
    Task<Subscription> AddExtraLicensesToSubscriptionAsync(int id, int extraLicenses);

    // Remove extra licenses from subscription
    Task<Subscription> RemoveExtraLicensesFromSubscriptionAsync(int id, int extraLicenses);

    // Calculate subscription price
    Task<Subscription> CalculateSubscriptionPriceAsync(int id);

    // Calculate subscription licenses used
    Task<Subscription> CalculateSubscriptionLicensesUsedAsync(int id);

    // Calculate subscription licenses available
    Task<Subscription> CalculateSubscriptionLicensesAvailableAsync(int id);

    // Upgrade subscription with new plan
    Task<Subscription> UpgradeSubscriptionAsync(int id, int planId);

    // Downgrade subscription with new plan
    Task<Subscription> DowngradeSubscriptionAsync(int id, int planId);

    // Cancel subscription
    Task<Subscription> CancelSubscriptionAsync(int id);

    // Reactivate subscription
    Task<Subscription> ReactivateSubscriptionAsync(int id);

    // Calculate next subscription billing date
    Task<Subscription> CalculateNextSubscriptionBillingDateAsync(int id);
}