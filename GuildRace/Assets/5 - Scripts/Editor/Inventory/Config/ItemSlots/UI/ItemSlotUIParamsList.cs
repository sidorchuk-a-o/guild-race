using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemSlotUIParamsList : ListElement<ItemSlotUIParams>
    {
        protected override Type ItemType { get; } = typeof(ItemSlotUIParamsItem);

        public override void BindData(SerializedData data)
        {
            base.BindData(data);

            Reorderable = false;
            ShowAddButton = false;
            ShowCloneButton = false;
            ShowRemoveButton = false;

            UpdateList();
        }

        private void UpdateList()
        {
            var itemTypes = InventoryEditorState.CreateItemTypesCollection();

            CreateOrUpdateList(itemTypes, getFilterId, initData);

            return;
            int getFilterId(ListItem<ItemSlotUIParams> item)
            {
                return item.Value.ItemType;
            }

            void initData(ItemSlotUIParams data, int id)
            {
                data.SetValue("itemType", (ItemType)id);
            }
        }
    }
}