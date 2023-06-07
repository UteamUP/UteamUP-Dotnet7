namespace UteamUP.Shared.ModelDto;

public class ToolTagDto
{
    public string Name { get; set; }
    public int? VendorId { get; set; }
    public int? CategoryId { get; set; }
    public ICollection<ToolTag> ToolTags { get; set; }
}