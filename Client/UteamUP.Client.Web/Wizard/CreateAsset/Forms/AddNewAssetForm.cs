namespace UteamUP.Client.Wizard.ActivateUser.Forms;

public class AddNewAssetForm
{
    public BasicAssetForm BasicAssetForm { get; set; }
    public AssetDetailsForm AssetDetailsForm { get; set; }
    public AssetUploadForm AssetUploadForm { get; set; }
    public AssetDescriptionForm AssetDescriptionForm { get; set; }
    public AssetNotesForm AssetNotesForm { get; set; }

    public AddNewAssetForm()
    {
        BasicAssetForm = new BasicAssetForm();
        AssetDetailsForm = new AssetDetailsForm();
        AssetUploadForm = new AssetUploadForm();
        AssetDescriptionForm = new AssetDescriptionForm();
        AssetNotesForm = new AssetNotesForm();
    }
}