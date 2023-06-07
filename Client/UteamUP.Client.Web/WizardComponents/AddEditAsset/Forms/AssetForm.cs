namespace UteamUP.Client.Web.WizardComponents.AddEditAsset.Forms;

public class AssetForm
{
    public AssetBasicForm AssetBasicForm { get; set; }

    public AssetForm()
    {
        AssetBasicForm = new AssetBasicForm();
    }
}