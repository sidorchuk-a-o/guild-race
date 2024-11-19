using AD.ToolsCollection;

namespace Game.Instances
{
    public class InstancesList : ListElement<InstanceData, InstanceItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Seasons";

            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}