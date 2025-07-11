﻿using AD.Services.Localization;
using AD.ToolsCollection;
using UniRx;

namespace Game.Inventory
{
    public abstract class ItemSlotInfo
    {
        private readonly ReactiveProperty<ItemInfo> item = new();

        public string Id { get; }
        public int DataId { get; }

        public ItemSlot Slot { get; }
        public LocalizeKey NameKey { get; }

        public bool HasItem => item.IsValid();
        public IReadOnlyReactiveProperty<ItemInfo> Item => item;

        public ItemSlotInfo(string id, ItemSlotData data)
        {
            Id = id;
            DataId = data.Id;
            Slot = data.Id;
            NameKey = data.NameKey;
        }

        public void SetItem(ItemInfo value)
        {
            value?.SetItemSlot(Id);

            item.Value = value;
        }

        public virtual bool CheckPossibilityOfPlacement(ItemInfo itemInfo)
        {
            return itemInfo.CheckSlotParams(this);
        }

        public virtual bool TryAddItem(ItemInfo item)
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

        public virtual bool TryRemoveItem()
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