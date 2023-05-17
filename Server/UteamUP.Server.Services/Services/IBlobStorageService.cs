using UteamUP.Shared.ModelDto;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UteamUP.Server.Services.Services;

public interface IBlobStorageService
{
    Task<FileUploadDto> UploadFile(IFormFile file, int tenantId, string oid, string type);
}