namespace UteamUP.Client.Web.Base;

public class UserComponentBase
{
    private readonly IDispatcher _dispatcher;
    private readonly ILogger<UserComponentBase> _logger;

    public UserComponentBase(IDispatcher dispatcher, ILogger<UserComponentBase> logger)
    {
        _dispatcher = dispatcher;
        _logger = logger;
    }

    public async Task<bool> SetUserDatabaseDetails(bool hasDatabaseUser)
    {
        try
        {
            _logger.Log(LogLevel.Information, $"Configuring if the user has database access : {hasDatabaseUser}");
            _dispatcher.Dispatch(new SetUserDatabaseAction(hasDatabaseUser));
            return true;
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return false;
        }
    }

    public async Task<bool> SetUserActivateDetails(bool hasActivatedUser)
    {
        try
        {
            _logger.Log(LogLevel.Information, $"Check if user has been activated : {hasActivatedUser}");
            _dispatcher.Dispatch(new SetUserActivateAction(hasActivatedUser));
            return true;
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return false;
        }
    }

    public async Task<bool> SetUserFirstLoginAction(bool isFirstLogin)
    {
        try
        {
            _logger.Log(LogLevel.Information, $"Check if it is the users first login : {isFirstLogin}");
            _dispatcher.Dispatch(new SetUserFirstLoginAction(isFirstLogin));
            return true;
        }
        catch (Exception e)
        {
            _logger.Log(LogLevel.Error, e.Message);
            return false;
        }
    }
}