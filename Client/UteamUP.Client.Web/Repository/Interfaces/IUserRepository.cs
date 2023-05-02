namespace UteamUP.Client.Web.Repository.Interfaces;

public interface IUserRepository
{
    Task<MUser?> GetUserByOid(string? oid);
    Task<MUserUpdateDto?> UpdateUserByOid(MUserUpdateDto? userUpdateDto, string oid);
}