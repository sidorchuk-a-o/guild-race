using AD.ToolsCollection;

namespace Game.Craft
{
    public class RecyclingReagentsList : ListElement<RecyclingReagentData, RecyclingReagentItem>
    {
        public override void BindData(SerializedData data)
        {
            showCloneButton = false;

            base.BindData(data);
        }
    }
}