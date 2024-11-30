using AD.ToolsCollection;

namespace Game.Inventory
{
    public class ReleaseHandlersList : ListElement<ReleaseHandler, ReleaseHandlerItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(ReleaseHandlerCreateWizard);
            showCloneButton = false;

            base.BindData(data);
        }
    }
}