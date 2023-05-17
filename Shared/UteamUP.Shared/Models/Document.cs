namespace UteamUP.Shared.Models;

public class Document : Base
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(255)]
    [MinLength(2)]
    public string Name { get; set; }
    public string Type { get; set; }
    public string UrlPath { get; set; }
    public bool IsActive { get; set; }
}