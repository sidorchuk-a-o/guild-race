using AD.ToolsCollection;

namespace Game.Guild
{
    public class ResourcesList : ListElement<ResourceData, ResourceItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(ResourceImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}