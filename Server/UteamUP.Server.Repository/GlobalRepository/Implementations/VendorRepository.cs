namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class VendorRepository : IVendorRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;

    public VendorRepository(IMapper mapper, pgContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Vendor> GetVendorAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Vendor>> GetVendorsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Vendor>> GetAllVendorsPaginatedAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
}