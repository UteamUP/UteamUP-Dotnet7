namespace UteamUP.Shared.Models;

public class Tag
{
    [Key] public int Id { get; set; }

    [MaxLength(512)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the name before you can save.")]
    public string Name { get; set; }

    public virtual List<Stock>? Stocks { get; set; }
    public virtual List<StockItemPart>? StockItemParts { get; set; }
    public virtual List<Asset>? Assets { get; set; }
    public virtual List<Part>? Parts { get; set; }
    public virtual List<Tool>? Tools { get; set; }
    public virtual List<Workorder>? Workorders { get; set; }
    public virtual List<MUser>? Users { get; set; }
    public virtual List<TagLocation>? TagLocations { get; set; }
}