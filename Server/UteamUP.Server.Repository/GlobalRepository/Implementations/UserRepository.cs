namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class MUserRepository : IMUserRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<MUserRepository> _logger;

    public MUserRepository(pgContext context, IMapper mapper, ILogger<MUserRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger ?? NullLogger<MUserRepository>.Instance;
    }

    public async Task<MUser?> GetByOidAsync(string oid)
    {
        // Check if oid is null or empty
        if (string.IsNullOrWhiteSpace(oid))
        {
            _logger.Log(LogLevel.Warning, $"Oid is null or empty");
            return new MUser();
        }

        // Check if the oid string is less than 30 characters
        if (oid.Length < 30)
        {
            _logger.Log(LogLevel.Warning, $"Oid characters are incorrect");
            return new MUser();
        }

        _logger.Log(LogLevel.Information, $"Trying to get user by oid {oid}");
        var user = await _context.Users.FirstOrDefaultAsync(x => x != null && x.Oid == oid);

        if (string.IsNullOrWhiteSpace(user?.Oid))
        {
            _logger.Log(LogLevel.Error, $"User not found");
            return new MUser();
        }

        return user;
    }

    public async Task<MUser> CreateUserAsync(MUserDto userDto)
    {
        // Create the user
        var user = _mapper.Map<MUser>(userDto);
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;

        _logger.Log(LogLevel.Information,
            $"Trying to create user {userDto.Name} with oid {userDto.Oid} at {user.CreatedAt}");

        // Add the user to the context
        _context.Users.Add(user);

        // Save the changes
        _context.SaveChanges();

        // Check if the user has been saved to the database
        if (!UserExists(user.Id))
        {
            _logger.Log(LogLevel.Error, $"Could not create user {userDto.Name} with oid {userDto.Oid}");
            return new MUser();
        }

        // Return the user
        return await Task.FromResult(user);
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
}