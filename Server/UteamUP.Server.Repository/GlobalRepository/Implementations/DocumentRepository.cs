using Microsoft.AspNetCore.Http;
using UteamUP.Server.Services.Services;

namespace UteamUP.Server.Repository.GlobalRepository.Implementations;

public class DocumentRepository : IDocumentRepository
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<DocumentRepository> _logger;
    private readonly IBlobStorageService _blobStorageService;
    
    
    
    public async Task<bool> UploadFiles(IList<IFormFile> files, int tenantId, string oid, string type)
    {
        try{
            if (files.Count == 0)
            {
                _logger.Log(LogLevel.Error, $"{nameof(UploadFiles)}: No files were uploaded since there were no files in the list");
                return false;
            }

            foreach (var file in files)
            {
                var filename = file.FileName;
                FileUploadDto fileUploadDto = await _blobStorageService.UploadFile(file, tenantId, oid, type);
                if (fileUploadDto == null)
                {
                    _logger.Log(LogLevel.Error, $"{nameof(UploadFiles)}: File upload failed for file {filename}");
                    return false;
                }else{
                    _logger.Log(LogLevel.Information, $"{nameof(UploadFiles)}: File upload succeeded for file {filename}");
                    await SaveDocumentToDbAsync(filename, type, fileUploadDto.UploadedFileUrl);
                }
            }
            
            return true;
        }catch(Exception ex){
            _logger.Log(LogLevel.Error, $"{nameof(UploadFiles)}: Exception thrown: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> SaveDocumentToDbAsync(string fileName, string type, string urlPath)
    {
        _logger.Log(LogLevel.Information, $"{nameof(SaveDocumentToDbAsync)}: Saving document to database");
        try
        {
            var document = new Document
            {
                Name = fileName,
                Type = type,
                UrlPath = urlPath,
                IsActive = true
            };
            
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Error, $"{nameof(SaveDocumentToDbAsync)}: Exception thrown: {ex.Message}");
            return false;
        }
    }
}