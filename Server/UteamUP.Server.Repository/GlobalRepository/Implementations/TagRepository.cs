namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class TagRepository : ITagRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<TagRepository> _logger;
    
    public TagRepository(pgContext context, IMapper mapper, ILogger<TagRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<List<Tag>> CreateAsync(List<TagDto> tags)
    {
        // Create a range of tags
        var mytags = _mapper.Map<List<Tag>>(tags);
        
        try{
            // Add the tags to the database
            _context.AddRange(mytags);
            
            // Save the changes
            _context.SaveChanges();
            
            // Return the tags
            return mytags;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Something went wrong while creating the tag");
            return new List<Tag>();
        }
    }
}