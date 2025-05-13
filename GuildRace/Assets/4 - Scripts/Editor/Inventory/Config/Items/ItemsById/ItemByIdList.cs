using AD.ToolsCollection;

namespace Game.Inventory
{
    public class ItemByIdList : ListElement<int, ItemIdItem>
    {
        public override void BindData(SerializedData data)
        {
            showCloneButton = false;

            base.BindData(data);
        }
    }
}