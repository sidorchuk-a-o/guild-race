using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemsGridCellTypesList : ListElement<ItemsGridCellTypeData>
    {
        protected override Type ItemType { get; } = typeof(ItemsGridCellTypeItem);
        protected override Type WizardType { get; } = typeof(ItemsGridCellTypeImportWizard);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Grid Cell Types";

            ShowCloneButton = false;
            ShowRemoveButton = false;
        }
    }
}