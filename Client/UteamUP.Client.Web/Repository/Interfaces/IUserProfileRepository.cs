using UteamUP.Shared.States;

namespace UteamUP.Client.Web.Repository.Interfaces;

public interface IUserProfileRepository
{
    Task<GlobalState?> GetUserProfile();
}