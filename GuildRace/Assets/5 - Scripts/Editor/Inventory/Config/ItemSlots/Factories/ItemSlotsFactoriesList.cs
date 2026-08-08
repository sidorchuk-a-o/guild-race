using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemSlotsFactoriesList : ListElement<ItemSlotsFactory>
    {
        protected override Type ItemType { get; } = typeof(ItemSlotsFactoryItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Item Slots Factories";

            Reorderable = false;
            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;

            CreateOrUpdateListByType<ItemSlotsFactory>();
        }
    }
}