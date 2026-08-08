using System.Collections.Generic;
using AD.Management;
using AD.Save;

namespace AD.Game.Inventory
{
    public class InventoryState : ServiceState<InventorySM>
    {
        private readonly Dictionary<string, ItemInfo> items = new();
        private readonly Dictionary<string, ItemSlotInfo> itemSlots = new();
        private readonly Dictionary<string, ItemsGridInfo> itemsGrids = new();

        public override string SaveKey => "inventory";
        public override SaveSource SaveSource => SaveSource.App;

        public IReadOnlyCollection<ItemInfo> Items => items.Values;
        public IReadOnlyCollection<ItemSlotInfo> ItemSlots => itemSlots.Values;
        public IReadOnlyCollection<ItemsGridInfo> ItemsGrids => itemsGrids.Values;

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
            return new();
        }

        protected override void CreateDefaultState()
        {
        }

        protected override void ReadSave(InventorySM save)
        {
        }
    }
}