using AD.Services;
using AD.Services.Save;
using System.Collections.Generic;
using VContainer;

namespace Game.Inventory
{
    public class InventoryState : ServiceState<InventoryConfig, InventorySM>
    {
        private readonly Dictionary<string, ItemInfo> items = new();
        private readonly Dictionary<string, ItemSlotInfo> itemSlots = new();
        private readonly Dictionary<string, ItemsGridInfo> itemsGrids = new();

        public override string SaveKey => InventorySM.key;
        public override SaveSource SaveSource => SaveSource.app;

        public InventoryState(InventoryConfig config, IObjectResolver resolver) : base(config, resolver)
        {
        }

        // == Items ==

        public ItemInfo GetItem(string id)
        {
            items.TryGetValue(id, out var info);

            return info;
        }

        public void AddItem(ItemInfo info)
        {
            if (info == null)
            {
                return;
            }

            items[info.Id] = info;
        }

        public ItemInfo RemoveItem(string id)
        {
            items.Remove(id, out var info);

            info?.MarkAsRemoved();

            return info;
        }

        // == Slots ==

        public ItemSlotInfo GetSlot(string id)
        {
            itemSlots.TryGetValue(id, out var item);

            return item;
        }

        public void AddSlot(ItemSlotInfo info)
        {
            if (info == null)
            {
                return;
            }

            itemSlots[info.Id] = info;
        }

        public ItemSlotInfo RemoveSlot(string id)
        {
            itemSlots.Remove(id, out var info);

            return info;
        }

        // == Grids ==

        public ItemsGridInfo GetGrid(string id)
        {
            itemsGrids.TryGetValue(id, out var item);

            return item;
        }

        public void AddGrid(ItemsGridInfo info)
        {
            if (info == null)
            {
                return;
            }

            itemsGrids[info.Id] = info;
        }

        public ItemsGridInfo RemoveGrid(string id)
        {
            itemsGrids.Remove(id, out var info);

            return info;
        }

        // == Save ==

        protected override InventorySM CreateSave()
        {
            return new InventorySM();
        }

        protected override void ReadSave(InventorySM save)
        {
        }
    }
}