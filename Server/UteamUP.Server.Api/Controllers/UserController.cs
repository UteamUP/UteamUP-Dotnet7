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
            _logger.Log(LogLevel.Error, $"Oid is null or empty");
            return new NoContentResult();
        }

        _logger.Log(LogLevel.Information, $"Getting user by oid {oid}");
        MUser nuser = new MUser();
        nuser.Oid = "bleh";
        nuser.Name = "bleh";
        nuser.Email = "bleh";
        nuser.Phone = "bleh";
        nuser.ActivationCode = "bleh";

        //var user = await _user.GetByOidAsync(oid);
        return Ok(nuser);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] MUserDto user)
    {
        if (string.IsNullOrWhiteSpace(user.Oid) ||
            string.IsNullOrWhiteSpace(user.Name) ||
            string.IsNullOrWhiteSpace(user.Email))
        {
            _logger.Log(LogLevel.Error, $"User data is null or empty");
            return new BadRequestResult();
        }

        var result = await _user.CreateUserAsync(user);
        return Ok(result);
    }
}