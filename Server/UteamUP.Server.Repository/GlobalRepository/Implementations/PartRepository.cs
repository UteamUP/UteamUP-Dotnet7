using Microsoft.AspNetCore.Http.Features;

namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class PartRepository : IPartRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<PartRepository> _logger;

    public PartRepository(IMapper mapper, pgContext context, ILogger<PartRepository> logger)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }


    public async Task<Part?> CreatePartAsync(PartDto part, int tenantId = 0)
    {
        bool partExists;
        if(tenantId != 0)
            partExists = await PartExistsByNameAndTenantAsync(part.Name, tenantId);
        else
            partExists = await PartExistsByNameAsync(part.Name);

        try
        {
            if (partExists)
                return _context.Parts.FirstOrDefault(x => x.Name == part.Name);
            
            // Map the part
            var mappedPart = _mapper.Map<Part>(part);

            mappedPart.CreatedAt = DateTime.Now.ToUniversalTime();
            mappedPart.UpdatedAt = DateTime.Now.ToUniversalTime();
            
            // Add the part
            _context.Parts.Add(mappedPart);
            
            // Save the changes
            _context.SaveChanges();
            
            return mappedPart;
        }catch(Exception e)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreatePartAsync)}: " + e.Message);
            return new Part();
        }
    }
    
    // Check if the plan already exists by name and tenant
    private async Task<bool> PartExistsByNameAndTenantAsync(string name, int tenantId)
    {
        return await _context.Parts.AnyAsync(x => x.Name == name && x.TenantId == tenantId);
    }
    
    // Check if the plan already exists by name
    private async Task<bool> PartExistsByNameAsync(string name)
    {
        return await _context.Parts.AnyAsync(x => x.Name == name);
    }
}