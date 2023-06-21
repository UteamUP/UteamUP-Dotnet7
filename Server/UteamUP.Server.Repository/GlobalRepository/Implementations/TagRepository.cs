using UteamUP.Shared.Results;

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
    
    public async Task<List<Tag>> CreateManyAsync(List<TagDto> tags)
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

    public async Task<TagDataResult<Tag>> GetAllTagsByTenantIdAsync(int tenantId, string filter, string sort, int skip, int top)
    {
        // Start with a query that selects from the Tags set where TenantId matches.
        var query = _context.Tags.Where(a => a.TenantId == tenantId);
    
        if (!string.IsNullOrEmpty(filter))
        {
            Console.WriteLine("The filter in repostiory is : " + filter);
            // Apply the filter to the query.
            // Here we're assuming the filter is a simple equality filter on the Name property.
            // Adjust this to your actual needs.
            
            // set filter to lowercase
            filter = filter.ToLower();
            
            // search all the names but search by contains with lowercase
            query = query.Where(t => t.Name.ToLower().Contains(filter));
        }

        // Get the total count for the paged result.
        var count = await query.CountAsync();

        if (!string.IsNullOrEmpty(sort))
        {
            // Apply the sort to the query.
            // Here we're assuming the sort is either "Name" for ascending or "Name desc" for descending.
            // Adjust this to your actual needs.
            query = sort.EndsWith(" desc") 
                ? query.OrderByDescending(t => t.Name) 
                : query.OrderBy(t => t.Name);
        }

        // Apply paging to the query.
        query = query.Skip(skip).Take(top);

        // Execute the query and get the result.
        var data = await query.ToListAsync();

        // Return the paged result.
        return new TagDataResult<Tag> { Data = data, Count = count };
    }

    public async Task<Tag> CreateAsync(Tag tag)
    {
        try
        {
            // Check if the tag already exists if so then return it
            var existingTag = await _context.Tags.FirstOrDefaultAsync(x => x.Name == tag.Name);
            if(existingTag != null) return existingTag;
            
            _context.Tags.Add(tag);
            
            _context.SaveChanges();
            
            return tag;
        }catch(Exception ex)
        {
            _logger.LogError(ex, $"{nameof(CreateAsync)}: Something went wrong while creating the tag");
            return new Tag();
        }
    }

    public async Task<Tag> GetTagByNameAsync(string name)
    {
        return await _context.Tags.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Tag> GetTagByNameAndLocationNameAsync(string name, string locationName)
    {
        // Find the tag by name and Location name which is a many to many field in tag and return it
        return await _context.Tags.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Tag> GetTagByNameAndTenantIdAsync(string tagName, int tenantId)
    {
        // Select from tag table where name is equal to the name of the tag, also select from TagLocation table where the location is equal to the tenant id
        var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Name == tagName);
        if (tag == null) return new Tag();
        return tag;
    }
}