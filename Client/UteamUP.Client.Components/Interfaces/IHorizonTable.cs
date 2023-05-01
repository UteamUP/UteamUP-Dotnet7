namespace UteamUP.Client.Components.Interfaces
{
    public interface IHorizonTable<TableItem>
    {
        List<IHorizonColumn<TableItem>> Columns { get; }
        void AddColumn(IHorizonColumn<TableItem> column);
        IEnumerable<TableItem> Items { get; }
        bool EnableGrouping { get; }
        int PageSize { get; }
        int PageNumber { get; }
        int TotalPages { get; }
        int TotalCount { get; }
        Task FirstPageAsync();
        Task NextPageAsync();
        Task PreviousPageAsync();
        Task LastPageAsync();
        Task NavigatePageAsync(int page);
    }
}
