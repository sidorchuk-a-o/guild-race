using AD.ToolsCollection;

namespace Game.Inventory
{
    public class ItemsGridCellTypesList : ListElement<ItemsGridCellTypeData, ItemsGridCellTypeItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Grid Cell Types";
            wizardType = typeof(ItemsGridCellTypeImportWizard);

            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }
    }
}