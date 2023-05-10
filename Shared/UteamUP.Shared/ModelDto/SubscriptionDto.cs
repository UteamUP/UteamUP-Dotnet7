namespace UteamUP.Shared.ModelDto;

public class SubscriptionDto
{
    public bool IsActive { get; set; } = true;
    public int ExtraAmountOfLicenses { get; set; }
    [ForeignKey("Tenant")] public int TenantId { get; set; }
}