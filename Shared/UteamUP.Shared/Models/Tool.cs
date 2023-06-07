namespace UteamUP.Shared.Models;

public class Tool : Base
{
    [Key] public int Id { get; set; }

    [MaxLength(512)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the name before you can save.")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
    public float? Width { get; set; }
    public float? Height { get; set; }
    public float? Length { get; set; }
    public float? Depth { get; set; }
    public float? Weight { get; set; }
    public float? Value { get; set; }
    public string? BarcodeNumber { get; set; }
    public string? SerialNumber { get; set; }
    public string? ReferenceNumber { get; set; }
    public string? ModelNumber { get; set; }
    public string? ToolNumber { get; set; }
    public string? AdditionalInfo { get; set; }
    public string? ImageUrl { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
    public float? MinPrice { get; set; }
    public float? MaxPrice { get; set; }
    public float? AvgPrice { get; set; }
    
    [ForeignKey("Vendor")] public int? VendorId { get; set; }
    public Vendor? Vendor { get; set; }
    
    [ForeignKey("Category")] public int? CategoryId { get; set; }
    public Category? Category { get; set; }
    public ICollection<ToolTag>? ToolTags { get; set; }
}