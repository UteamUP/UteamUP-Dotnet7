namespace UteamUP.Shared.ModelDto;

public class StockTagDto
{
    public StockDto Stock { get; set; }
    public string Location { get; set; }
    public string Category { get; set; }
    public List<TagDto> Tags { get; set; }
}