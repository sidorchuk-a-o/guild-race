using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemsGridCategoriesList : ListElement<ItemsGridCategoryData>
    {
        protected override Type ItemType { get; } = typeof(ItemsGridCategoryItem);
        protected override Type WizardType { get; } = typeof(ItemsGridCategoryImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Grid Categories";

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}