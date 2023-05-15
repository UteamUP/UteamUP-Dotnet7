namespace UteamUP.Shared.Models;

public class Tool : Base
{
    public Tool()
    {
        Tags = new List<Tag>();
    }

    [Key] public int Id { get; set; }

    [MaxLength(512)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the name before you can save.")]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public float Width { get; set; }
    public float Height { get; set; }
    public float Length { get; set; }
    public float Depth { get; set; }
    public float Weight { get; set; }
    public float Value { get; set; }
    public string BarcodeNumber { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string ReferenceNumber { get; set; } = string.Empty;
    public string ModelNumber { get; set; } = string.Empty;
    public string ToolNumber { get; set; } = string.Empty;
    public string AdditionalInfo { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool IsPrivate { get; set; }
    public bool IsActive { get; set; }
    public bool IsDestroyed { get; set; }
    public bool IsLost { get; set; }

    // TenantID can be empty if IsPrivate is false, if IsPrivate is true, then Tenant is required
    [ForeignKey("Tenant")] public int? TenantId { get; set; }
    
    [ForeignKey("Vendor")] public int? VendorId { get; set; }
    public Vendor? Vendor { get; set; }
    
    [ForeignKey("Category")] public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public virtual List<Tag>? Tags { get; set; } = new();
}