namespace UteamUP.Shared.ModelDto;

public class PartDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string ReferenceNumber { get; set; } = string.Empty;
    public string ModelNumber { get; set; } = string.Empty;
    public string PartNumber { get; set; } = string.Empty;
    public string AdditionalInfo { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public bool IsPrivate { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}