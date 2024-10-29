using AD.Services.Localization;
using AD.ToolsCollection;
using UniRx;

namespace Game.Inventory
{
    public class ItemSlotInfo
    {
        private readonly ReactiveProperty<ItemInfo> item = new();

        public string Id { get; }
        public ItemSlot Slot { get; }
        public LocalizeKey NameKey { get; }

        public bool HasItem => item.IsValid();
        public IReadOnlyReactiveProperty<ItemInfo> Item => item;

        public ItemSlotInfo(string id, ItemSlotData data)
        {
            Id = id;
            Slot = data.Id;
            NameKey = data.NameKey;
        }

        public void SetItem(ItemInfo value)
        {
            item.Value = value;
            item.Value?.SetItemSlot(Id);
        }

        public bool CheckPossibilityOfPlacement(ItemInfo itemInfo)
        {
            return !HasItem && itemInfo.Slot == Slot;
        }

        public bool TryAddItem(ItemInfo item)
        {
            var check = CheckPossibilityOfPlacement(item);

            if (check)
            {
                if (item.Bounds.IsRotated)
                {
                    item.Bounds.Rotate();
                }

                SetItem(item);
            }

            return check;
        }

        public bool TryRemoveItem()
        {
            var check = HasItem;

            if (check)
            {
                SetItem(null);
            }

            return check;
        }
    }
}