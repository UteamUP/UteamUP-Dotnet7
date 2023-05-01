namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface IMUserRepository
{
    // Create user will use the generic create repository method
    // Update user will use the generic update repository method

    Task<MUser?> GetByOidAsync(string oid);
    Task<MUser> CreateUserAsync(MUserDto userDto);

    //Task<MUser> ActivateUserAsync(string oid, string code);
    //Task<MUser> DisableUserAsync(string oid);

    // Get all users by tenant paginated
    //Task<IEnumerable<MUser>> GetAllUsersPaginatedAsync(int tenantId, int pageNumber, int pageSize);
}