using AD.ToolsCollection;

namespace Game.Instances
{
    public class InstanceTypesList : ListElement<InstanceTypeData, InstanceTypeItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Instance Types";
            wizardType = typeof(InstanceTypeImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}