namespace UteamUP.Shared.ModelDto;

public class SubscriptionDto
{
    public string Guid { get; set; }
    public bool IsActive { get; set; }
    public int ExtraAmountOfLicenses { get; set; }
    public int TenantId { get; set; }
    public int PlanId { get; set; }

}