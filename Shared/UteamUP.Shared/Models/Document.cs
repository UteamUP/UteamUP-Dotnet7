namespace UteamUP.Shared.Models;

public class Document : Base
{
    public Document()
    {
        Tags = new List<Tag>();
    }

    [Key] public int Id { get; set; }

    [MaxLength(512)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the name before you can save.")]
    public string Name { get; set; } = string.Empty;

    [ForeignKey("Category")] public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public virtual List<Tag>? Tags { get; set; } = new();
}