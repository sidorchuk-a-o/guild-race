using AD.ToolsCollection;

namespace Game.Inventory
{
    public abstract class ItemSlotsList<TData> : ListElement<TData, ItemSlotItem>
        where TData : ItemSlotData
    {
        public override void BindData(SerializedData data)
        {
            showCloneButton = false;

            base.BindData(data);
        }
    }
}