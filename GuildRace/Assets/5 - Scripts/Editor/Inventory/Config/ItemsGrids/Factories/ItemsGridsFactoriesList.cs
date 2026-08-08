using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemsGridsFactoriesList : ListElement<ItemsGridsFactory>
    {
        protected override Type ItemType { get; } = typeof(ItemsGridsFactoryItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Item Grids Factories";

            Reorderable = false;
            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;

            CreateOrUpdateListByType<ItemsGridsFactory>();
        }
    }
}