using System.Security.Claims;
using UteamUP.Shared.States;

namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface IClaimRepository
{
    Task<UserState> GetUserState(ClaimsPrincipal principal);
    Task<bool> ValidateUser(ClaimsPrincipal principal);
}