namespace UteamUP.Shared.Models;

public class Subscription : Base
{
    [Key] public int Id { get; set; }

    public string Guid { get; set; }
    public bool IsActive { get; set; } = true;
    public int ExtraAmountOfLicenses { get; set; }

    [ForeignKey("Plan")] public int PlanId { get; set; }
    public Plan? Plan { get; set; }

    [ForeignKey("Tenant")] public int TenantId { get; set; }
    public Tenant? Tenant { get; set; }
}