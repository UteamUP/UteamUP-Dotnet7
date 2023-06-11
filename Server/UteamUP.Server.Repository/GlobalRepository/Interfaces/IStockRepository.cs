namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface IStockRepository
{
    Task<StockTagDto?> GetByStockId(int stockId);
    Task<List<Stock>> GetStockByTenantId(int tenantId);

    Task<Stock> UpdateStockWithTags(StockTagDto stockItems, int stockId);

    Task<Stock?> CreateStockWithTags(StockTagDto stockItems);
}