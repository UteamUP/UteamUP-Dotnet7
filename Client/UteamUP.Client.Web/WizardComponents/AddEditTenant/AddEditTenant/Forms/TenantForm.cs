using UteamUP.Client.Web.WizardComponents.Template.AddEditTenant.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Forms;

public class TenantForm
{
    public TenantAddressForm TenantAddressForm { get; set; }
    public TenantLicensesForm TenantLicensesForm { get; set; }
    public TenantPlanForm TenantPlanForm { get; set; }
    public TenantBasicForm TenantBasicForm { get; set; }
    public TenantOwnerForm TenantOwnerForm { get; set; }
    

    public TenantForm()
    {
        TenantBasicForm = new TenantBasicForm();
        TenantAddressForm = new TenantAddressForm();
        TenantLicensesForm = new TenantLicensesForm();
        TenantPlanForm = new TenantPlanForm();
        TenantOwnerForm = new TenantOwnerForm();
    }
}