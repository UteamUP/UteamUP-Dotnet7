namespace UteamUP.Client.Web.WizardComponents.AddEditCategory.Forms;

public class CategoryForm
{
    public CategoryBasicForm CategoryBasicForm { get; set; }

    public CategoryForm()
    {
        CategoryBasicForm = new CategoryBasicForm();
    }
}