namespace UteamUP.Shared.Models;

public class StockItemPart : Base
{
    [Key] public int Id { get; set; }
    public Stock? Stock { get; set; }
    public Part? Part { get; set; }
    public int MinimumAmount { get; set; }
    public int CurrentAmount { get; set; }

    [ForeignKey("Category")] public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    //public virtual HashSet<Tag>? Tags { get; set; } = new();
}