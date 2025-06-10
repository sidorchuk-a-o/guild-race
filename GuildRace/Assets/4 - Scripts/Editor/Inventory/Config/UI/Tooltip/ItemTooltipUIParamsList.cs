using System.Collections.Generic;
using System.Linq;
using AD.ToolsCollection;

namespace Game.Inventory
{
    public class ItemTooltipUIParamsList : ListElement<ItemTooltipUIParams, ItemTooltipUIParamsItem>
    {
        public override void BindData(SerializedData data)
        {
            UpdateList(data);

            reorderable = false;
            showAddButton = false;
            showCloneButton = false;
            showRemoveButton = false;

            base.BindData(data);
        }

        private void UpdateList(SerializedData data)
        {
            var list = data.GetValue<List<ItemTooltipUIParams>>(new());
            var itemTypes = InventoryEditorState.CreateItemTypesCollection();

            // remove old items
            var removedItems = list
                .Where(x => !itemTypes.Any(t => t.value == x.ItemType))
                .ToListPool();

            if (removedItems.Any())
            {
                var saveMeta = new SaveMeta(isSubObject, data);

                foreach (var item in removedItems)
                {
                    DataFactory.Remove(item, saveMeta);
                }

                data.MarkAsDirty();
            }

            // create new items
            var newItemTypes = itemTypes
                .Where(x => !list.Any(t => t.ItemType == x.value))
                .ToListPool();

            if (newItemTypes.Any())
            {
                var saveMeta = new SaveMeta(isSubObject, data);

                foreach (var (option, value) in newItemTypes)
                {
                    var newItem = DataFactory.Create(typeof(ItemTooltipUIParams), saveMeta);

                    newItem.SetValue("itemType", (ItemType)value);
                }

                data.MarkAsDirty();
            }

            removedItems.ReleaseListPool();
            newItemTypes.ReleaseListPool();
        }
    }
}