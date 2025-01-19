using AD.ToolsCollection;

namespace Game.Craft
{
    public class VendorsList : ListElement<VendorData, VendorItem>
    {
        public override void BindData(SerializedData data)
        {
            wizardType = typeof(VendorImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}