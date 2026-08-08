using System;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemGridUIParamsList : ListElement<ItemGridUIParams>
    {
        protected override Type ItemType { get; } = typeof(ItemGridUIParamsItem);

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
            int getFilterId(ListItem<ItemGridUIParams> item)
            {
                return item.Value.ItemType;
            }

            void initData(ItemGridUIParams data, int id)
            {
                data.SetValue("itemType", (ItemType)id);
            }
        }
    }
}