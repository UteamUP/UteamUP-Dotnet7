using Microsoft.AspNetCore.Http;

namespace UteamUP.Client.Web.Services;

public interface IBlobStorageWebService
{
    //Task<FileUploadDto> UploadFile(IFormFile file, int tenantId, string oid, string type);
    Task<UploadFileDto> UploadImagesAsync(string blobContainerName, Stream content, string fileName, string blobPath);

}