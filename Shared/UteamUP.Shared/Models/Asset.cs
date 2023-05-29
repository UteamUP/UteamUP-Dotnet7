namespace UteamUP.Shared.Models;

public class Asset : Base
{

    [Key] public int Id { get; set; }

    [MaxLength(512)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the location name before you can save.")]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string ReferenceNumber { get; set; } = string.Empty;
    public string ModelNumber { get; set; } = string.Empty;
    public string UpcNumber { get; set; } = string.Empty;
    public string AdditionalInfo { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string CheckInProcedure { get; set; } = string.Empty;
    public string CheckOutProcedure { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public MUser Creator { get; set; } // To show who created the category

    [ForeignKey("Tenant")] public int? TenantId { get; set; }
    public Tenant? Tenant { get; set; }

    [ForeignKey("Vendor")] public int? VendorId { get; set; }
    public Vendor? Vendor { get; set; }

    [ForeignKey("Category")] public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public virtual List<Part>? Parts { get; set; } = new();
    //public virtual List<Location>? Locations { get; set; } = new();
    //public virtual HashSet<Tag>? Tags { get; set; } = new();
}