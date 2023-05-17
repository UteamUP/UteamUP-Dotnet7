namespace UteamUP.Shared.Models;

public class Report : Base
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public string Status { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime ReportDate { get; set; }
    public DateTime TimeSpent { get; set; }
    public int HoursSpent { get; set; }

    [ForeignKey("Workorder")] public int WorkorderID { get; set; }
    public Workorder Workorder { get; set; }
    
    public virtual ICollection<MUser>? Users { get; set; }
}