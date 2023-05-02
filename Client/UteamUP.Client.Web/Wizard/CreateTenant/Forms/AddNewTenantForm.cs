namespace UteamUP.Client.Wizard.CreateTenant.Forms;

public class AddNewTenantForm
{
    public BasicTenantDetailsForm BasicTenantDetailsForm { get; set; }
    public PlanDetailsForm PlanDetailsForm { get; set; }
    public ExtraLicensesForm ExtraLicensesForm { get; set; }

    public AddNewTenantForm()
    {
        BasicTenantDetailsForm = new BasicTenantDetailsForm();
        PlanDetailsForm = new PlanDetailsForm();
        ExtraLicensesForm = new ExtraLicensesForm();
    }
}