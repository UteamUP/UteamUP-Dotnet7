namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class WorkorderRepository : IWorkorderRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;

    public WorkorderRepository(IMapper mapper, pgContext context)
    {
        _mapper = mapper;
        _context = context;
    }
}