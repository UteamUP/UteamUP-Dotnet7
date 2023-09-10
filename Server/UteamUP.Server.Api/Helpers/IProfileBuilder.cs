using UteamUP.Shared.States;

namespace UteamUP.Server.Api.Helpers;

public interface IProfileBuilder
{
    Task<GlobalState?> GetUserProfile(string oid);

}