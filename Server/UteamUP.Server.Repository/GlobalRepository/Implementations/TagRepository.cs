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

    public async Task<Tag> GetTagByNameAsync(string name)
    {
        return await _context.Tags.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Tag> GetTagByNameAndLocationNameAsync(string name, string locationName)
    {
        // Find the tag by name and Location name which is a many to many field in tag and return it
        return await _context.Tags.FirstOrDefaultAsync(x => x.Name == name && x.TagLocations.Where(y => y.Location.Name == locationName).Any());
    }

    public async Task<Tag> GetTagByNameAndTenantIdAsync(string tagName, int tenantId)
    {
        // Select from tag table where name is equal to the name of the tag, also select from TagLocation table where the location is equal to the tenant id
        return await _context.Tags.FirstOrDefaultAsync(x => x.Name == tagName && x.TagLocations.Where(y => y.Location.TenantId == tenantId).Any());
    }
}