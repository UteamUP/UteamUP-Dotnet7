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
            _logger.Log(LogLevel.Warning, $"GetByOidAsync: Oid is null or empty");
            return new MUser();
        }

        // Check if the oid string is less than 30 characters
        if (oid.Length < 30)
        {
            _logger.Log(LogLevel.Warning, $"GetByOidAsync: Oid characters are incorrect");
            return new MUser();
        }

        _logger.Log(LogLevel.Information, $"GetByOidAsync: Trying to get user by oid {oid}");
        var user = await _context.Users.FirstOrDefaultAsync(x => x != null && x.Oid == oid);

        if (string.IsNullOrWhiteSpace(user?.Oid))
        {
            _logger.Log(LogLevel.Error, $"GetByOidAsync: User not found");
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
            $"CreateUserAsync: Trying to create user {userDto.Name} with oid {userDto.Oid} at {user.CreatedAt}");

        // Add the user to the context
        _context.Users.Add(user);

        // Save the changes
        _context.SaveChanges();

        // Check if the user has been saved to the database
        if (!UserExists(user.Id))
        {
            _logger.Log(LogLevel.Error, $"CreateUserAsync: Could not create user {userDto.Name} with oid {userDto.Oid}");
            return new MUser();
        }

        // Return the user
        return await Task.FromResult(user);
    }

    public async Task<MUserUpdateDto> UpdateUserAsync(MUserUpdateDto userDto, string oid)
    {
        if (!UserExistByOid(oid))
            return new MUserUpdateDto();
        
        // Get the user
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Oid == oid);
            _logger.Log(LogLevel.Information, $"UpdateUserAsync: Updating user {userDto.Name} with oid {oid}");

            // Update the values where needed
            if (!string.IsNullOrWhiteSpace(userDto.Name))
                user.Name = userDto.Name;

            if (!string.IsNullOrWhiteSpace(userDto.Email))
                user.Email = userDto.Email;

            if (!string.IsNullOrWhiteSpace(userDto.DepartmentCompany))
                user.DepartmentCompany = userDto.DepartmentCompany;

            if (!string.IsNullOrWhiteSpace(userDto.Phone))
                user.Phone = userDto.Phone;

            if (!string.IsNullOrWhiteSpace(userDto.DefaultTenantId.ToString()))
                user.DefaultTenantId = userDto.DefaultTenantId;

            if (!string.IsNullOrWhiteSpace(userDto.Country))
                user.Country = userDto.Country;

            if (!string.IsNullOrWhiteSpace(userDto.StreetName))
                user.StreetName = userDto.StreetName;

            if (!string.IsNullOrWhiteSpace(userDto.City))
                user.City = userDto.City;
            
            if (!string.IsNullOrWhiteSpace(userDto.PostalCode))
                user.PostalCode = userDto.PostalCode;

            if (!string.IsNullOrWhiteSpace(userDto.Website))
                user.Website = userDto.Website;

            if (!string.IsNullOrWhiteSpace(userDto.IsAdmin.ToString()))
                user.IsAdmin = userDto.IsAdmin;
            
            if (!string.IsNullOrWhiteSpace(userDto.IsActive.ToString()))
                user.IsActive = userDto.IsActive;

            if (!string.IsNullOrWhiteSpace(userDto.IsDeleted.ToString()))
                user.IsDeleted = userDto.IsDeleted;

            if (!string.IsNullOrWhiteSpace(userDto.IsFirstLogin.ToString()))
                user.IsFirstLogin = userDto.IsFirstLogin;

            if (!string.IsNullOrWhiteSpace(userDto.HasAcceptedLicenseAgreement.ToString()))
                user.HasAcceptedLicenseAgreement = userDto.HasAcceptedLicenseAgreement;
            
            if (!user.HasBeenActivated || string.IsNullOrWhiteSpace(user.ActivationCode))
            {
                // Generate Activation Code
                var activationCode = Guid.NewGuid().ToString();
                user.ActivationCode = activationCode;
            }
            
            if(user.ActivationCode == userDto.ActivationCode)
                user.HasBeenActivated = true;

            // Save the changes
            await _context.SaveChangesAsync();

            // Return the user
            return userDto;
        }catch(Exception e)
        {
            _logger.Log(LogLevel.Error, $"UpdateUserAsync: Could not update user {userDto.Name} with oid {oid}");
            return new MUserUpdateDto();
        }
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
    
    private bool UserExistByOid(string oid)
    {
        return _context.Users.Any(e => e.Oid == oid);
    }
}