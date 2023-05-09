namespace UteamUP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class VendorController : ControllerBase
{
    private readonly ILogger<VendorController> _logger;
    private readonly IVendorRepository _vendor;

    public VendorController(IVendorRepository vendor, ILogger<VendorController> logger)
    {
        _vendor = vendor;
        _logger = logger;
    }
    
    private async Task<MUserDto> ValidateUser()
    {
        // Get the oid from the user who is logged in
        var oid = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? User.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
        // Get email from the user who is logged in
        var email = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/signInNames.emailAddress")?.Value ?? User.Claims.FirstOrDefault(c => c.Type == "signInNames.emailAddress")?.Value;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(oid))
            return new MUserDto();

        MUserDto mUser = new MUserDto();
        mUser.Email = email;
        mUser.Oid = oid;
        
        // Validate that the user is admin
        return mUser;
    }
    
    // Get all vendors
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        MUserDto mUser = await ValidateUser();
        if (string.IsNullOrWhiteSpace(mUser.Email) || string.IsNullOrWhiteSpace(mUser.Oid))
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetAllAsync)}: User is not valid");
            return BadRequest();
        }

        _logger.Log(LogLevel.Information, $"{nameof(GetAllAsync)}: Getting all vendors for user with oid {mUser.Oid}");
        var vendors = await _vendor.GetAllAsync();
        if (vendors == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetAllAsync)}: Vendors were not found");
            return NotFound();
        }

        return Ok(vendors);
    }

    // Create a vendor
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] VendorDto vendor)
    {
        if (vendor == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateAsync)}: Vendor is null");
            return BadRequest();
        }

        MUserDto mUser = await ValidateUser();
        if (string.IsNullOrWhiteSpace(mUser.Email) || string.IsNullOrWhiteSpace(mUser.Oid))
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateAsync)}: User is not valid");
            return BadRequest();
        }

        _logger.Log(LogLevel.Information, $"{nameof(CreateAsync)}: Creating vendor for user with oid {mUser.Oid}");
        var createdVendor = await _vendor.CreateAsync(vendor, mUser.Oid);
        if (createdVendor == null)
        {
            _logger.Log(LogLevel.Error, $"{nameof(CreateAsync)}: Vendor was not created");
            return BadRequest();
        }

        return Ok(createdVendor);
    }
    
}