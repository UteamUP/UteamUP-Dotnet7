namespace UteamUP.Shared.ModelDto;

public class FileUploadDto
{
    public string UploadedFileUrl = string.Empty;
    public List<string> Errors { get; set; } = new();
}