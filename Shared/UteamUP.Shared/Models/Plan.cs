namespace UteamUP.Shared.Models;

public class Plan : Base
{
    [Key] public int Id { get; set; }

    [MaxLength(255)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the name before you can save.")]
    public string Name { get; set; }

    [MaxLength(255)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the sku for the plan before you can save.")]
    public string Sku { get; set; }

    public string? Description { get; set; }
    public string? PlanAgreement { get; set; }

    [Required(ErrorMessage = "You must set the plan type.")]
    public int PlanType { get; set; }

    [Required(ErrorMessage = "You must set amount of licenses included in this plan.")]
    public int LicenseIncluded { get; set; }

    [Required(ErrorMessage = "You must set price per license for this plan.")]
    public float PricePerLicense { get; set; }

    public float ExtraDiscountPerUser { get; set; }
    public DateTime DiscountExpiry { get; set; }
}