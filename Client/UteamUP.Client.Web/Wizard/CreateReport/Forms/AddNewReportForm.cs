namespace UteamUP.Client.Wizard.ActivateUser.Forms;

public class AddNewReportForm
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new();
}