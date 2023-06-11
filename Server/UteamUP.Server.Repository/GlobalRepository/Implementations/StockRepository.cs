namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class StockRepository : IStockRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<StockRepository> _logger;
    
    public StockRepository(
        IMapper mapper, 
        pgContext context, 
        ILogger<StockRepository> logger
        )
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }
    
    public async Task<StockTagDto?> GetByStockId(int stockId)
    {
        if(stockId == 0) throw new Exception("StockId cannot be 0.");
        
        StockTagDto? stock = await _context.Stocks
            .Include(s => s.StockTags)
            .ThenInclude(st => st.Tag)
            .Where(s => s.Id == stockId)
            .Select(s => new StockTagDto
            {
                Stock = new StockDto
                {
                    Name = s.Name,
                    Guid = s.Guid,
                    RackBarNumber = s.RackBarNumber,
                    ShelveNumber = s.ShelveNumber,
                    ShelveName = s.ShelveName,
                    TenantId = s.TenantId,
                    LocationId = s.LocationId,
                    CategoryId = s.CategoryId
                },
                Tags = s.StockTags.Select(t => new TagDto
                {
                    TenantId = t.Tag.TenantId,
                    Name = t.Tag.Name
                }).ToList()
            })
            .FirstOrDefaultAsync();
        
        return stock;
    }

    public async Task<List<Stock>> GetStockByTenantId(int tenantId)
    {
        if(tenantId == 0) throw new Exception("TenantId cannot be 0.");
        
        return await _context.Stocks
            .Where(s => s.TenantId == tenantId)
            .ToListAsync();
    }

    public async Task<Stock> UpdateStockWithTags(StockTagDto stockItems, int stockId)
    {
        // Search for the stock in the database, do not include tags
        var existingStock = await _context.Stocks
            .Include(s => s.StockTags)
            .ThenInclude(st => st.Tag)
            .FirstOrDefaultAsync(s => s.Id == stockId && s.TenantId == stockItems.Stock.TenantId);
        
        // If the stock is null return empty stock
        if(existingStock == null)
            return new Stock();
        
        // Update the stock
        existingStock.Name = stockItems.Stock.Name;
        //existingStock.Guid = stockItems.Stock.Guid;
        existingStock.RackBarNumber = stockItems.Stock.RackBarNumber;
        existingStock.ShelveNumber = stockItems.Stock.ShelveNumber;
        existingStock.ShelveName = stockItems.Stock.ShelveName;
        existingStock.TenantId = stockItems.Stock.TenantId;
        existingStock.LocationId = stockItems.Stock.LocationId;
        existingStock.CategoryId = stockItems.Stock.CategoryId;
        existingStock.UpdatedAt = DateTime.Now.ToUniversalTime();
        
        // Update the stock
        _context.Stocks.Update(existingStock);
        
        // Save the changes
        await _context.SaveChangesAsync();
        
        // Compare stock and tags and add new tags that are not in existingStock
        foreach(var tag in stockItems.Tags)
        {
            // Check if the tag does not exists in the database then add it
            if(!_context.Tags.Any(t => t.Name == tag.Name && t.TenantId == stockItems.Stock.TenantId))
            {
                Tag newTag = new();
                newTag.Name = tag.Name;
                newTag.TenantId = stockItems.Stock.TenantId;
                newTag.CreatedAt = DateTime.Now.ToUniversalTime();
                newTag.UpdatedAt = DateTime.Now.ToUniversalTime();
                await _context.Tags.AddAsync(newTag);
                await _context.SaveChangesAsync();
            }
            
            // Check if the tag does not exists in the stock then add it
            if(!existingStock.StockTags.Any(t => t.Tag.Name == tag.Name))
            {
                Tag newTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tag.Name && t.TenantId == stockItems.Stock.TenantId);
                StockTag stockTag = new();
                stockTag.Stock = existingStock;
                stockTag.Tag = newTag;
                
                await _context.StockTags.AddAsync(stockTag);
                await _context.SaveChangesAsync();
            }
        }

        // Get existing stock with tags again to see if the tags were updated
        var updatedStock = await _context.Stocks
            .Include(s => s.StockTags)
            .ThenInclude(st => st.Tag)
            .FirstOrDefaultAsync(s => s.Id == stockId && s.TenantId == stockItems.Stock.TenantId);
        
        // Compare tags and stock and remove tags in existingStock that are not in tags
        foreach(var tag in updatedStock.StockTags)
        {
            // Check if the tag does not exists in the tags then remove it
            if(!stockItems.Tags.Any(t => t.Name == tag.Tag.Name))
            {
                _context.StockTags.Remove(tag);
                await _context.SaveChangesAsync();
            }
        }

        return updatedStock;
    }

    public async Task<Stock?> CreateStockWithTags(StockTagDto stockItems)
    {
        // Check if the stock already exists
        var stockExists = await StockExistsByNameAndTenantAsync(stockItems.Stock.Name, stockItems.Stock.TenantId);
        if(stockExists)
            return _context.Stocks.FirstOrDefault(x => x.Name == stockItems.Stock.Name && x.TenantId == stockItems.Stock.TenantId);
        
        // Map stockItems.Stock to Stock
        var stock = _mapper.Map<Stock>(stockItems.Stock);
        
        stock.Guid = Guid.NewGuid().ToString();
        
        // Map stockItems.Tags to List<Tag>
        var tags = _mapper.Map<List<Tag>>(stockItems.Tags);
        
        // set the created at and updated at on stock
        stock.CreatedAt = DateTime.Now.ToUniversalTime();
        stock.UpdatedAt = DateTime.Now.ToUniversalTime();
        
        // set the created at and updated at on tags
        tags.ForEach(x =>
        {
            x.TenantId = stock.TenantId;
            x.CreatedAt = DateTime.Now.ToUniversalTime();
            x.UpdatedAt = DateTime.Now.ToUniversalTime();
        });
        
        // Add the stock
        await _context.Stocks.AddAsync(stock);
        
        // Save the changes
        await _context.SaveChangesAsync();
        
        // Get the stock with the tag
        var existingStock = await _context.Stocks
            .Include(s => s.StockTags)
            .ThenInclude(st => st.Tag)
            .FirstOrDefaultAsync(s => s.Id == stock.Id && s.TenantId == stockItems.Stock.TenantId);
        
        // Loop through existingStock.Tags and check if the tag exists in the database, if exists create it, if not add it to the stock.
        foreach (var tag in stockItems.Tags)
        {
            if (!_context.Tags.Any(t => t.Name == tag.Name && t.TenantId == stockItems.Stock.TenantId))
            {
                Tag newTag = new();
                newTag.Name = tag.Name;
                newTag.TenantId = stock.TenantId;
                newTag.CreatedAt = DateTime.Now.ToUniversalTime();
                newTag.UpdatedAt = DateTime.Now.ToUniversalTime();
                
                await _context.Tags.AddAsync(newTag);
                await _context.SaveChangesAsync();
            }

            if (!existingStock.StockTags.Any(t => t.Tag.Name == tag.Name))
            {
                Tag newTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tag.Name && t.TenantId == stockItems.Stock.TenantId);
                
                StockTag stockTag = new();
                
                stockTag.Stock = existingStock;
                stockTag.Tag = newTag;
                
                await _context.StockTags.AddAsync(stockTag);
                await _context.SaveChangesAsync();
            }

            return existingStock;

        }
        



        return new Stock();
    }
    
    // Check if the stock already exists by name and tenant
    private async Task<bool> StockExistsByNameAndTenantAsync(string name, int tenantId)
    {
        return await _context.Stocks.AnyAsync(x => x.Name == name && x.TenantId == tenantId);
    }
}