namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface IMUserRepository
{
    Task<MUser?> GetByOidAsync(string oid);
    Task<MUser> CreateUserAsync(MUserDto userDto);
    Task<MUserUpdateDto> UpdateUserAsync(MUserUpdateDto userDto, string oid);
    Task<bool> ActivateUserAsync(string oid, string activationCode);
    Task<MUser?> UpdateDefaultTenantId(int tenantId, string oid);
}