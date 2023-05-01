namespace UteamUP.Shared.Models;

public class ItemList : Base
{
    [Key] public int Id { get; set; }
    public ICollection<ItemListItem>? ItemListItems { get; set; }
}