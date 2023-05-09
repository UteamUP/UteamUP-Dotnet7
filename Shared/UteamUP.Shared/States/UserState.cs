using UteamUP.Shared.ModelDto;

namespace UteamUP.Shared.States;

public class UserState
{
    public MUser User { get; private set; }

    public void SetUser(MUser user)
    {
        User = user;
    }
}