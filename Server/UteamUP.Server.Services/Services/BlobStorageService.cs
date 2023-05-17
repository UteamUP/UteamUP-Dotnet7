using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UteamUP.Shared.ModelDto;

namespace UteamUP.Server.Services.Services;

public class BlobStorageService : IBlobStorageService
{
    private readonly string storageConnectionString;
    
    private readonly ILogger<BlobStorageService> logger;
    
    public BlobStorageService(
        IConfiguration configuration, 
        ILogger<BlobStorageService> logger
        )
    {
        this.logger = logger;
        this.storageConnectionString = "DefaultEndpointsProtocol=https;EndpointSuffix=core.windows.net;AccountName=uteamupstorage;AccountKey=Qz3UWKK1nAqR+J3dOFjXIVUwiHHbITDNLyJLE8kV7nfHoKsWOxynGIzPcJQ0M/j5vtNx6OUmuSHUCTo4IiKKxQ==";
    }

    public async Task<FileUploadDto> UploadFile(IFormFile file, int tenantId, string oid, string type)
    {
        FileUploadDto fileUploadResultDto = new();
        try
        {
            BlobServiceClient clientStorageAccount = new(this.storageConnectionString);
            BlobContainerClient blobContainer = clientStorageAccount.GetBlobContainerClient(type);
            await blobContainer.CreateIfNotExistsAsync();
            BlobClient blockBlob;
            var fileName = file.FileName;
            var blobPath = $"{tenantId.ToString()}/{oid}/{fileName}";

            blockBlob = blobContainer.GetBlobClient(blobPath);

            var blobHttpHeaders = new BlobHttpHeaders()
            {
                ContentType = file.ContentType
            };

            await blockBlob.UploadAsync(file.OpenReadStream(), blobHttpHeaders);

            fileUploadResultDto.UploadedFileUrl = blockBlob.Uri.ToString();
            return fileUploadResultDto;
        }catch(Exception ex)
        {
            fileUploadResultDto.Errors.Add(ex.Message);
        }

        return fileUploadResultDto;
    }
}