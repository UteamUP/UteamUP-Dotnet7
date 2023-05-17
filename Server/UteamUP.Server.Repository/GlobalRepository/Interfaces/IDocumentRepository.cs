using Microsoft.AspNetCore.Http;

namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface IDocumentRepository
{
    Task<bool> UploadFiles(IList<IFormFile> files, int tenantId, string oid, string type);
    Task<bool> SaveDocumentToDbAsync(string fileName, string type, string urlPath);
}