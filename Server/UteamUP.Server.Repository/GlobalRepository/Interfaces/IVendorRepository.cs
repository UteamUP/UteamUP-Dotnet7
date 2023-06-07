namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface IVendorRepository
{
    Task<Vendor?> CreateAsync(VendorDto vendor, string oid);
    Task<Vendor?> UpdateAsync(int id, VendorDto vendor, string oid);
    Task<List<Vendor>> GetAllAsync();
    Task<VendorDto> GetByIdAsync(int id);

    //Task<Vendor> GetVendorAsync(int id);
    //Task<IEnumerable<Vendor>> GetVendorsAsync();

    // Get all vendors paginated
    //Task<IEnumerable<Vendor>> GetAllVendorsPaginatedAsync(int pageNumber, int pageSize);

    // Create vendor will use the generic create repository method
    // Update vendor will use the generic update repository method
    // Delete vendor will use the generic delete repository method
}