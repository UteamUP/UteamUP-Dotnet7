using System.Security.Claims;
using UteamUP.Shared.States;

namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class ClaimRepository : IClaimRepository
{
    private readonly pgContext _context;
    private UserState userClaimData = new UserState();
    private readonly ILogger<ClaimRepository> _logger;
    
    public ClaimRepository(ILogger<ClaimRepository> logger)
    {
        _logger = logger;
    }
    
    private async Task<string> GetDataFromPrincipalAsync(ClaimsPrincipal principal, string searchValue)
    {
        try
        {
            return principal.Claims.FirstOrDefault(x => x.Type == searchValue).Value.ToString();
        }
        catch
        {
            _logger.Log(LogLevel.Error, $"{nameof(GetDataFromPrincipalAsync)}: Error getting data from principal for " + searchValue);
            return null;
        }
    }
    
    public async Task<UserState> GetUserState(ClaimsPrincipal principal)
    {
        userClaimData.Email = await GetDataFromPrincipalAsync(principal, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/signInNames.emailAddress");

        if(userClaimData.Email == null) 
            userClaimData.Email = await GetDataFromPrincipalAsync(principal, "signInNames.emailAddress");

        userClaimData.Name = await GetDataFromPrincipalAsync(principal, "name");
        userClaimData.OID = await GetDataFromPrincipalAsync(principal, "http://schemas.microsoft.com/identity/claims/objectidentifier");

        var data = userClaimData;
        return data;
    }

    public async Task<bool> ValidateUser(ClaimsPrincipal User)
    {
        try{
            // Get the user
            //var user = GetUserState(principal).Result;
            
            
            
            var oid = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value ?? User.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
            userClaimData.OID = oid;
            var email = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/signInNames.emailAddress")?.Value ?? User.Claims.FirstOrDefault(c => c.Type == "signInNames.emailAddress")?.Value;
            userClaimData.Email = email;
            
            var user = userClaimData;
                
            // Check if the user email and oid exist
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.OID))
                return false;
            
            // Check if the user exists in the database and get the user details
            var dbUser = _context.Users.FirstOrDefault(x => x.Email == user.Email && x.Oid == user.OID);
            
            // If the user does not exist then return false
            if (dbUser == null)
                return false;
            
            // if the user exists and has the IsAdmin flag set to true then return true
            if (!dbUser.IsAdmin)
                return false;

            // Return true if the user is valid
            return true;
        }
        catch(Exception ex)
        {
            _logger.Log(LogLevel.Error, $"{nameof(ValidateUser)}: Error validating user: " + ex.Message);
            return false;
        }
    }
}