namespace UteamUP.Client.Web.WizardComponents.AddEditTool.Forms;

public class ToolBasicForm
{
    public string Name { get; set; }
    
    public int? VendorId { get; set; }
    public string? VendorName { get; set; }
    public int? CategoryId { get; set; }
    public string? CategoryName { get; set; }

    public bool? IsActive { get; set; } = true;
    public List<Tag> Tags { get; set; }
}