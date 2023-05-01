namespace UteamUP.Shared.Models;

public class StockItem : Base
{
    public StockItem()
    {
        Tags = new List<Tag>();
    }

    [Key] public int Id { get; set; }
    public Stock? Stock { get; set; }
    public Part? Part { get; set; }
    public int MinimumAmount { get; set; }
    public int CurrentAmount { get; set; }

    [ForeignKey("Category")] public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public virtual List<Tag>? Tags { get; set; } = new();
}