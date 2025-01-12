using AD.ToolsCollection;

namespace Game.Instances
{
    public class InstancesList : ListElement<InstanceData, InstanceListItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Instances";

            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}