using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using UteamUP.Client.Components.Models;

namespace UteamUP.Client.Components.Interfaces
{
    public interface IHorizonColumn<TableItem>
    {
        IHorizonTable<TableItem> Table { get; set; }
        string Title { get; set; }
        Icon TitleIcon { get; set; }
        Expression<Func<TableItem, object>> Field  { get; set; }
        string Render(TableItem data);
        RenderFragment<TableItem> Template { get; set; }
    }
}
