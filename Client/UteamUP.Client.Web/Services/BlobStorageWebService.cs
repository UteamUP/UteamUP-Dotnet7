using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace UteamUP.Client.Web.Services;

public class BlobStorageWebService : IBlobStorageWebService
{
    private readonly string storageConnectionString;
    private readonly ILogger<BlobStorageWebService> logger;
    
    public BlobStorageWebService(
        IConfiguration configuration,
        ILogger<BlobStorageWebService> logger
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

            if(oid != null || oid != "all") { 
                blockBlob = blobContainer.GetBlobClient(blobPath);
            }
            else {
                blockBlob = blobContainer.GetBlobClient($"{fileName}");
            }

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