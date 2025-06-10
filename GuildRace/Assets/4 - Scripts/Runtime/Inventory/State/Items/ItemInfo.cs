using AD.Services.Localization;
using AD.ToolsCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine.AddressableAssets;

namespace Game.Inventory
{
    public abstract class ItemInfo : IEquatable<ItemInfo>
    {
        private readonly ReactiveProperty<bool> isRemoved = new();

        public string Id { get; }
        public int DataId { get; }
        public ItemType ItemType { get; }

        public LocalizeKey NameKey { get; }
        public AssetReference IconRef { get; }
        public ItemBoundsInfo Bounds { get; }

        public IReadOnlyList<ItemsGridCategory> GridCategories { get; private set; }
        public IReadOnlyList<ItemsGridCellType> GridCellTypes { get; private set; }

        public ItemSlot Slot { get; }
        public string SlotId { get; private set; }
        public bool IsPlacedInSlot { get; private set; }

        public IReadOnlyReactiveProperty<bool> IsRemoved => isRemoved;

        public ItemInfo(string id, ItemData data)
        {
            Id = id;
            DataId = data.Id;
            ItemType = data.ItemType;
            NameKey = data.NameKey;
            IconRef = data.IconRef;
            Bounds = new(data.Size);
            Slot = data.Slot;
        }

        public void SetGridParams(GridParamsForItems gridParams)
        {
            GridCategories = gridParams.Categories;
            GridCellTypes = gridParams.CellTypes;
        }

        public void SetItemSlot(string slotId)
        {
            SlotId = slotId;
            IsPlacedInSlot = slotId.IsValid();
        }

        public void MarkAsRemoved()
        {
            isRemoved.Value = true;
        }

        // == Check Params ==

        public virtual bool CheckGridParams(ItemsGridInfo itemsGrid)
        {
            return GridCategories.Contains(itemsGrid.Category)
                && GridCellTypes.Contains(itemsGrid.CellType);
        }

        public virtual bool CheckSlotParams(ItemSlotInfo itemSlot)
        {
            return Slot == itemSlot.Slot;
        }

        // == IEquatable ==

        public bool Equals(ItemInfo other)
        {
            return other is not null
                && other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            return obj is ItemInfo item
                && Equals(item);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(ItemInfo a, ItemInfo b)
        {
            return a is null || b is null ? Equals(a, b) : a.Equals(b);
        }

        public static bool operator !=(ItemInfo a, ItemInfo b)
        {
            return !(a == b);
        }
    }
}