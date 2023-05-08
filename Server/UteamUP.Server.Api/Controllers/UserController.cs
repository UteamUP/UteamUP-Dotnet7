using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;

namespace UteamUP.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    //private readonly IEmailService _emailService;
    private readonly ILogger<UserController> _logger;
    private readonly IMUserRepository _user;
    private readonly IMapper _mapper;

    public UserController(
        IMUserRepository user,
        //IEmailService emailService,
        ILogger<UserController> logger, IMapper mapper)
    {
        _user = user;
        //_emailService = emailService;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet("oid/{oid}")]
    public async Task<IActionResult> GetByOidAsync(string oid)
    {
        if (string.IsNullOrWhiteSpace(oid))
        {
            _logger.Log(LogLevel.Error, $"GetByOidAsync: Oid is null or empty");
            return new NoContentResult();
        }

        _logger.Log(LogLevel.Information, $"GetByOidAsync: Getting user by oid {oid}");

        var user = await _user.GetByOidAsync(oid);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] MUserDto user)
    {
        if (string.IsNullOrWhiteSpace(user.Oid) ||
            string.IsNullOrWhiteSpace(user.Name) ||
            string.IsNullOrWhiteSpace(user.Email))
        {
            _logger.Log(LogLevel.Error, $"PostAsync: User data is null or empty");
            return new BadRequestResult();
        }

        var result = await _user.CreateUserAsync(user);
        return Ok(result);
    }
    
    // Activate user with post
    [HttpPost("oid/{oid}/activate/{activationCode}")]
    public async Task<IActionResult> ActivateUserAsync(string oid, string activationCode)
    {
        if (string.IsNullOrWhiteSpace(oid) ||
            string.IsNullOrWhiteSpace(activationCode))
        {
            _logger.Log(LogLevel.Error, $"{nameof(ActivateUserAsync)}: User data is null or empty");
            return new BadRequestResult();
        }

        var result = await _user.ActivateUserAsync(oid, activationCode);
        if(result == false)
            return BadRequest("Activation code is not valid");
        
        _logger.Log(LogLevel.Information, $"{nameof(ActivateUserAsync)}: User activated");
        return Ok(result);
    }
    
    [HttpPut("oid/{oid}")]
    public async Task<IActionResult> PutAsync([FromBody] MUserUpdateDto user, string oid)
    {
        if (string.IsNullOrWhiteSpace(oid) ||
            string.IsNullOrWhiteSpace(user.Name) ||
            string.IsNullOrWhiteSpace(user.Email))
        {
            _logger.Log(LogLevel.Error, $"{nameof(PutAsync)}: User data is null or empty");
            return new BadRequestResult();
        }

        var result = await _user.UpdateUserAsync(user, oid);
        _logger.Log(LogLevel.Information, $"{nameof(PutAsync)}: User updated");
        return Ok(result);
    }
}