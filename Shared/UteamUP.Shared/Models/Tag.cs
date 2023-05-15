namespace UteamUP.Shared.Models;

public class Tag
{
    public Tag()
    {
        Stocks = new List<Stock>();
        StockItemParts = new List<StockItemPart>();
        Assets = new List<Asset>();
        Parts = new List<Part>();
        Workorders = new List<Workorder>();
        Users = new List<MUser>();
    }

    [Key] public int Id { get; set; }

    [MaxLength(512)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the name before you can save.")]
    public string Name { get; set; }

    public virtual List<Stock>? Stocks { get; set; } = new();
    public virtual List<StockItemPart>? StockItemParts { get; set; } = new();
    public virtual List<Asset>? Assets { get; set; } = new();
    public virtual List<Part>? Parts { get; set; } = new();
    public virtual List<Tool>? Tools { get; set; } = new();
    public virtual List<Workorder>? Workorders { get; set; } = new();
    public virtual List<MUser>? Users { get; set; } = new();
}