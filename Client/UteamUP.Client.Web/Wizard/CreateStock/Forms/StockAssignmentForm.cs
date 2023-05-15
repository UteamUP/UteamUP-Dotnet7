namespace UteamUP.Client.Wizard.ActivateUser.Forms;

public class StockAssignmentForm
{
    public virtual List<Location>? Locations { get; set; }
    public virtual List<Tag>? Tags { get; set; } = new();
    public Tenant? Tenant { get; set; }
    public string Category { get; set; }

}