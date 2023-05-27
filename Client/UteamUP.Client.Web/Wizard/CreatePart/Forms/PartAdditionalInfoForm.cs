using UteamUP.Client.Components.Inputs;

namespace UteamUP.Client.Web.Wizard.CreatePart.Forms;

public class PartAdditionalInfoForm
{
    public string AdditionalInfo { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public HorizonFileUpload FileUpload { get; set; } = new();
}