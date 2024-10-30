using AD.ToolsCollection;

namespace Game.Inventory
{
    public class ItemsGridCategoriesList : ListElement<ItemsGridCategoryData, ItemsGridCategoryItem>
    {
        public override void BindData(SerializedData data)
        {
            headerTitle = "Grid Categories";
            wizardType = typeof(ItemsGridCategoryCreateWizard);

            showCloneButton = false;

            base.BindData(data);
        }
    }
}