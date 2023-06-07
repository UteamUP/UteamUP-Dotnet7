using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditAsset.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditAsset.Validators;

public class AssetBasicValidator : AbstractValidator<AssetBasicForm>
{
    public AssetBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}