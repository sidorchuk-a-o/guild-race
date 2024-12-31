using AD.ToolsCollection;

namespace Game.Guild
{
    public class SpecializationsList : ListElement<SpecializationData, SpecializationItem>
    {
        public override void BindData(SerializedData data)
        {
            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}