using AD.ToolsCollection;

namespace Game.Craft
{
    public class ReagentItemsList : ListElement<ReagentItemData, ReagentItemItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Reagents";

            showCloneButton = false;
            showRemoveButton = false;

            wizardType = typeof(ReagentItemImportWizard);

            base.BindData(data);
        }
    }
}