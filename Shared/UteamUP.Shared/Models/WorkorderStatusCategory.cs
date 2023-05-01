namespace UteamUP.Shared.Models;

public class WorkorderStatusCategory : Base
{
    [Key] public int Id { get; set; }

    [MaxLength(255)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the workorder category name.")]
    public string Name { get; set; }
}