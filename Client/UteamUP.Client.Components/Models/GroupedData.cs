namespace UteamUP.Client.Components.Models
{
    public class GroupedData<TableItem> 
    {
        public object Key { get; set; }
        public string PropertyName { get; set; }
        public TableItem Data { get; set; }
        public List<GroupedData<TableItem>> Children { get; set; }
    }
}
