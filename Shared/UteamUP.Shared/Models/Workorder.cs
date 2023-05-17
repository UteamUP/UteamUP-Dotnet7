namespace UteamUP.Shared.Models;

public class Workorder : Base
{
    public Workorder()
    {
        Tags = new List<Tag>();
    }

    [Key] public int Id { get; set; }

    [MaxLength(512)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the name before you can save.")]
    public string Name { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int Priority { get; set; }
    public bool StopRequired { get; set; } = false;
    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }

    public virtual List<Tag>? Tags { get; set; } = new();

    public string AssignedUserName { get; set; }
    public string Assignee { get; set; } = string.Empty;
    
    public virtual ICollection<Part>? Parts { get; set; }
    
}