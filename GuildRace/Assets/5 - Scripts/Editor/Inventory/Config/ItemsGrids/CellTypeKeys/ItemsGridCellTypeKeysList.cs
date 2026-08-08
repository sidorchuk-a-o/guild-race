using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemsGridCellTypeKeysList : ListElement<ItemsGridCellType>
    {
        protected override Type ItemType { get; } = typeof(ItemsGridCellTypeKeyItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Grid Cell Types";
            ShowCloneButton = false;
        }
    }
}