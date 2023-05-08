namespace UteamUP.Client.Web.Repository.Interfaces;

public interface IUserWebRepository
{
    Task<MUser?> GetUserByOid(string? oid);
    Task<MUserUpdateDto?> UpdateUserByOid(MUserUpdateDto? userUpdateDto, string oid);
    Task<bool> ActivateUser(string activationCode, string oid);
}