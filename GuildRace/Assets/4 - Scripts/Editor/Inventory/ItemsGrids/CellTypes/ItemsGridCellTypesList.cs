using AD.ToolsCollection;

namespace Game.Inventory
{
    public class ItemsGridCellTypesList : ListElement<ItemsGridCellTypeData, ItemsGridCellTypeItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Grid Cell Types";
            wizardType = typeof(ItemsGridCellTypeCreateWizard);

            showCloneButton = false;

            base.BindData(data);
        }
    }
}