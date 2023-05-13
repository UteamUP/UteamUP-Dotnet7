namespace UteamUP.Client.Web.Repository.Interfaces;

public interface IUserWebRepository
{
    Task<MUser?> GetUserByOid(string? oid);
    Task<bool> CreateUserAsync(MUserDto user);
    Task<MUserUpdateDto?> UpdateUserByOid(MUserUpdateDto? userUpdateDto, string oid);
    Task<MUser?> UpdateDefaultTenantId(int tenantId, string oid);
    Task<bool> ActivateUser(string activationCode, string oid);
}