using AD.ToolsCollection;

namespace Game.Instances
{
    public class ConsumableMechanicHandlersList : ListElement<ConsumableMechanicHandler, ConsumableMechanicHandlerItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Consumable Mechanics";
            wizardType = typeof(ConsumableMechanicHandlerImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}