namespace UteamUP.Shared.ModelDto;

public class AssetDto
{
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
    public Tenant? Tenant { get; set; }
    public Vendor? Vendor { get; set; }
    public Category? Category { get; set; }
    public virtual List<Part>? Parts { get; set; } = new();
    public virtual List<Location>? Locations { get; set; } = new();
    public virtual List<Tag>? Tags { get; set; } = new();
}