using AD.ToolsCollection;

namespace Game.Inventory
{
    public class PickupHandlersList : ListElement<PickupHandler, PickupHandlerItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(PickupHandlerCreateWizard);
            showCloneButton = false;

            base.BindData(data);
        }
    }
}