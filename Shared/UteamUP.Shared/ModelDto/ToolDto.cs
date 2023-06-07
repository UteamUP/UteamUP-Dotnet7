namespace UteamUP.Shared.ModelDto;

public class ToolDto
{
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
    public bool? IsActive { get; set; }
    public float? MinPrice { get; set; }
    public float? MaxPrice { get; set; }
    public float? AvgPrice { get; set; }
    public int? VendorId { get; set; }
    public int? CategoryId { get; set; }
    public List<string?>? Tags { get; set; }
}