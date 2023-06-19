namespace UteamUP.Shared.ModelDto;

public class StockDto
{
    public string Name { get; set; }
    public string? Guid { get; set; }
    public string RackBarNumber { get; set; }
    public string ShelveNumber { get; set; }
    public string ShelveName { get; set; }

    public int TenantId { get; set; }
    
    public int? LocationId { get; set; }

    public int? CategoryId { get; set; }
    
    public List<string?>? Tags { get; set; }
}