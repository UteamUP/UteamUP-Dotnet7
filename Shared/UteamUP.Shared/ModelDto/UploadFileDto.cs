namespace UteamUP.Shared.ModelDto;

public class UploadFileDto
{
    public string FileName { get; set; }
    public string FileUrl { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}