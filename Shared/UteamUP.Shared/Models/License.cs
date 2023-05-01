namespace UteamUP.Shared.Models;

public class License : Base
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; }
    public int MaxLicenses { get; set; }
    public int MinLicenses { get; set; }

    [ForeignKey("Tenant")] public int TenantId { get; set; }
    public Tenant Tenant { get; set; }

    [ForeignKey("Subscription")] public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }

    [ForeignKey("Plan")] public int PlanId { get; set; }
    public Plan Plan { get; set; }
    public virtual ICollection<MUser>? Users { get; set; }
}