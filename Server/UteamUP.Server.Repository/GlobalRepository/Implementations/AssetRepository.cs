namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class AssetRepository : IAssetRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;

    public AssetRepository(IMapper mapper, pgContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<AssetDto>> GetAllAssetsPaginatedAsync(int pageNumber, int pageSize)
    {
        // Use fast query that does not holds the the thread lock
        var query = await _context.Assets
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Map the query to the DTO
        var result = _mapper.Map<IEnumerable<AssetDto>>(query);

        return result;
    }
}