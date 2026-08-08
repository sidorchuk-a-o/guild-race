using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemsFactoriesList : ListElement<ItemsFactory>
    {
        protected override Type ItemType { get; } = typeof(ItemsFactoryItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            HeaderTitle = "Items Factories";

            Reorderable = false;
            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;

            CreateOrUpdateListByType<ItemsFactory>(initItem: item =>
            {
                item.SetValue("title", item.Title.Clear("Factory"));
            });
        }
    }
}