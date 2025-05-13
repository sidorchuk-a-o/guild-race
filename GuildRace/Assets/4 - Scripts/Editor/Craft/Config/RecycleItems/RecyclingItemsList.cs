using AD.ToolsCollection;

namespace Game.Craft
{
    public class RecyclingItemsList : ListElement<RecyclingItemData, RecyclingItemItem>
    {
        public override void BindData(SerializedData data)
        {
            showCloneButton = false;

            base.BindData(data);
        }
    }
}