namespace UteamUP.Client.Wizard.CreatePlan.Forms;

public class AddNewPlanForm
{
    public BasicPlanForm BasicPlanForm { get; set; }
    public PlanCostForm PlanCostForm { get; set; }
    public PlanAgreementForm PlanAgreementForm { get; set; }
    public PlanDescriptionForm PlanDescriptionForm { get; set; }

    public AddNewPlanForm()
    {
        BasicPlanForm = new BasicPlanForm();
        PlanCostForm = new PlanCostForm();
        PlanAgreementForm = new PlanAgreementForm();
        PlanDescriptionForm = new PlanDescriptionForm();
    }
}