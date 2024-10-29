using AD.ToolsCollection;

namespace Game.Inventory
{
    public class OptionHandlersList : ListElement<OptionHandler, OptionHandlerItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Context Menu Handlers";
            wizardType = typeof(OptionHandlerCreateWizard);

            showCloneButton = false;

            base.BindData(data);
        }
    }
}