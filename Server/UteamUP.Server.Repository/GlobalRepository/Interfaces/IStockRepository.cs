namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface IStockRepository
{
    Task<StockTagDto?> GetByStockId(int stockId, int tenantId);
    Task<List<Stock>> GetStockByTenantId(int tenantId);

    Task<Stock> UpdateStockWithTags(StockDto stockItem, int stockId);

    Task<Stock?> CreateStockWithTags(StockDto stockDto);
}