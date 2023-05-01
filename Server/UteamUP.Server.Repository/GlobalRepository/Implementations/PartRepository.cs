namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class PartRepository : IPartRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;

    public PartRepository(IMapper mapper, pgContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<Part>> GetAssetPartByAssetIdPaginatedAsync(int assetId, int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Part>> GetAllPartsPaginatedAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    public async Task<Part> GetPartByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}