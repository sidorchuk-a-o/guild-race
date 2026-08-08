using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemsGridCategoryKeysList : ListElement<ItemsGridCategory>
    {
        protected override Type ItemType { get; } = typeof(ItemsGridCategoryKeyItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Grid Categories";
            ShowCloneButton = false;
        }
    }
}