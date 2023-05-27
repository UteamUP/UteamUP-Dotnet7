using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace UteamUP.Client.Web.Services;

public class BlobStorageWebService : IBlobStorageWebService
{
    private readonly string storageConnectionString;
    private readonly ILogger<BlobStorageWebService> logger;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=uteamupimages;AccountKey=PdHI3TjGHrfNiTXyUTa8KywrYPjLAgwNy5HJ0r01wKyDHXe9NRBwV7VfMkpown9oBcE05JeRPwev+AStpcPOmg==;EndpointSuffix=core.windows.net";
    
    public BlobStorageWebService(
        IConfiguration configuration,
        ILogger<BlobStorageWebService> logger)
    {
        this.logger = logger;
        _blobServiceClient = new BlobServiceClient(_connectionString);
        this.storageConnectionString = _connectionString;
    }

    public async Task<UploadFileDto> UploadImagesAsync(string blobContainerName, Stream content, string fileName, string blobPath)
    {
        UploadFileDto fileUploadResultDto = new();

            fileUploadResultDto.FileName = fileName;
            BlobClient blockBlob;

            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);
            Console.WriteLine($"{nameof(UploadImagesAsync)}: 1. " + blobContainerClient.Name);
            
            blockBlob = blobContainerClient.GetBlobClient(blobPath);
            fileUploadResultDto.FileUrl = blockBlob.Uri.ToString();
            
            // If folder does not exist then create it
            
            Console.WriteLine(blockBlob.BlobContainerName + " " + blockBlob.Name + " " + blockBlob.Uri);
            if(await blockBlob.ExistsAsync())
            {
                Console.WriteLine($"{nameof(UploadImagesAsync)}: 3. The file already exists in blob storage {blobContainerName}");

                var blobProperties = await blockBlob.GetPropertiesAsync();
                var blobLastModified = blobProperties.Value.LastModified;
                
                if(blobLastModified > DateTime.Now.AddMinutes(-1))
                {
                    Console.WriteLine($"{nameof(UploadImagesAsync)}: 4. The file is newer than the file in blob storage, uploading newer file {fileName} to blob storage {blobContainerName}");

                    logger.Log(LogLevel.Information, $"{nameof(UploadImagesAsync)}: The file is newer than the file in blob storage, uploading newer file {fileName} to blob storage {blobContainerName}");
                    await blockBlob.UploadAsync(content);
                    fileUploadResultDto.IsSuccess = true;
                    fileUploadResultDto.Message = "The file is newer than the file in blob storage";
                    return fileUploadResultDto;
                }

                logger.Log(LogLevel.Information, $"{nameof(UploadImagesAsync)}: The file {fileName} already exists in blob storage {blobContainerName}");
                fileUploadResultDto.Message = "The file already exists. No need to upload.";
                fileUploadResultDto.IsSuccess = false;
                
                return fileUploadResultDto;
            }
            else
            {
                Console.WriteLine($"{nameof(UploadImagesAsync)}: 5. The file does not exist in blob storage {blobContainerName}");

                logger.Log(LogLevel.Information, $"{nameof(UploadImagesAsync)}: Uploading file {fileName} to blob storage {blobContainerName}");
                await blockBlob.UploadAsync(content);
                fileUploadResultDto.IsSuccess = true;
                fileUploadResultDto.Message = "The file is uploaded";

                return fileUploadResultDto;
            }
        /*}
        catch(Exception ex)
        {
            logger.Log(LogLevel.Warning, $"{nameof(UploadImagesAsync)}: Error : {ex.Message}");
        }*/
        
        fileUploadResultDto.IsSuccess = false;
        fileUploadResultDto.Message = "Something went wrong";

        return fileUploadResultDto;
    }
}