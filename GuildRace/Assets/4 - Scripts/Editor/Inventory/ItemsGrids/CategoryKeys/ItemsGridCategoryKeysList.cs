using AD.ToolsCollection;

namespace Game.Inventory
{
    public class ItemsGridCategoryKeysList : ListElement<ItemsGridCategory, ItemsGridCategoryKeyItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Grid Categories";
            showCloneButton = false;

            base.BindData(data);
        }
    }
}