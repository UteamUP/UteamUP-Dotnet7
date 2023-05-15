namespace UteamUP.Shared.Models;

public class Part : Base
{
    public Part()
    {
        Tags = new List<Tag>();
    }

    [Key] public int Id { get; set; }

    [MaxLength(512)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the location name before you can save.")]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string ReferenceNumber { get; set; } = string.Empty;
    public string ModelNumber { get; set; } = string.Empty;
    public string PartNumber { get; set; } = string.Empty;
    public string AdditionalInfo { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool IsPrivate { get; set; }
    
    // TenantID can be empty if IsPrivate is false, if IsPrivate is true, then Tenant is required
    [ForeignKey("Tenant")] public int? TenantId { get; set; }
    
    [ForeignKey("Vendor")] public int? VendorId { get; set; }
    public Vendor? Vendor { get; set; }

    [ForeignKey("Category")] public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public virtual List<Tag>? Tags { get; set; } = new();
}