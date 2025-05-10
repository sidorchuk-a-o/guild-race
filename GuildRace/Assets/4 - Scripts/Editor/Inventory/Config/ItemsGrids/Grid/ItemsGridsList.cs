using AD.ToolsCollection;

namespace Game.Inventory
{
    public abstract class ItemsGridsList<TData> : ListElement<TData, ItemsGridItem>
        where TData : ItemsGridData
    {
        public override void BindData(SerializedData data)
        {
            showCloneButton = false;

            base.BindData(data);
        }
    }
}