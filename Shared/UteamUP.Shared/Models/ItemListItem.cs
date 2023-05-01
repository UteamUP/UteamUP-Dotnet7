namespace UteamUP.Shared.Models;

public class ItemListItem : Base
{
    [Key] public int Id { get; set; }

    [ForeignKey("ItemList")] public int? ItemListId { get; set; }
    public ItemList? ItemList { get; set; }
}