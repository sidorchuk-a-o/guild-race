using AD.ToolsCollection;

namespace Game.Instances
{
    public class ConsumableTypesList : ListElement<ConsumableTypeData, ConsumableTypeItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Consumable Types";
            wizardType = typeof(ConsumableTypeImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}