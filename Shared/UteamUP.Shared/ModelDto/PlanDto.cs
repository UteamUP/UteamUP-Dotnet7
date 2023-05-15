namespace UteamUP.Shared.ModelDto;

public class PlanDto
{
    public string Name { get; set; }
    public string Sku { get; set; }
    public string? Description { get; set; }
    public string? PlanAgreement { get; set; }
    public string PlanType { get; set; }
    public int LicenseIncluded { get; set; }
    public float PricePerLicense { get; set; }
    public float ExtraDiscountPerUser { get; set; }
    public DateTime DiscountExpiry { get; set; }
    public bool IsActive { get; set; }
}