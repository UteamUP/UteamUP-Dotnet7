namespace UteamUP.Client.Wizard.CreatePlan.Forms;

public class PlanCostForm
{
    public int LicenseIncluded { get; set; }
    public float PricePerLicense { get; set; }
    public float ExtraDiscountPerUser { get; set; }
    public DateTime DiscountExpiry { get; set; }
}