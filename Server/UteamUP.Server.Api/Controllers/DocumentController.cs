namespace UteamUP.Server.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DocumentController : ControllerBase
{
    private readonly ILogger<DocumentController> _logger;
    private readonly IDocumentRepository _documentRepository;
    
    public DocumentController(
        ILogger<DocumentController> logger, 
        IDocumentRepository documentRepository
        )
    {
        _logger = logger;
        _documentRepository = documentRepository;
    }
    
    // Upload files to Azure Blob Storage
    [HttpPost("oid/{oid}/tenant/{tenantId}/type/{type}")]
    public async Task<IActionResult> Upload(IList<IFormFile> UploadFiles, string oid, int tenantId, string type)
    {
        if (UploadFiles.Count == 0)
            return BadRequest("No file found");

        foreach(var file in UploadFiles)
        {
            var filename = file.FileName;
            bool uploadResult = await this._documentRepository.UploadFiles(UploadFiles, tenantId, oid, type);
        }

        return Ok();
    }
}