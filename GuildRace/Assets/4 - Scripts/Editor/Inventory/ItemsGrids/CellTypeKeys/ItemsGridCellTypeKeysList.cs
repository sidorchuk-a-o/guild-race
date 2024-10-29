using AD.ToolsCollection;

namespace Game.Inventory
{
    public class ItemsGridCellTypeKeysList : ListElement<ItemsGridCellType, ItemsGridCellTypeKeyItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Grid Cell Types";
            showCloneButton = false;

            base.BindData(data);
        }
    }
}