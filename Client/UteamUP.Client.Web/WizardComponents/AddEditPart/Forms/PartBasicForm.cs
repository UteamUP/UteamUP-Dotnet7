namespace UteamUP.Client.Web.WizardComponents.AddEditPart.Forms;

public class PartBasicForm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public int? CategoryId { get; set; }
    public string Vendor { get; set; }
    public int? VendorId { get; set; }
}