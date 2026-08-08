using System;
using System.Collections.Generic;
using System.Linq;
using AD.Localization;
using AD.ToolsCollection;
using R3;
using UnityEngine.AddressableAssets;

namespace AD.Game.Inventory
{
    public abstract class ItemInfo : IEquatable<ItemInfo>
    {
        private readonly ReactiveProperty<bool> isRemoved = new();

        public string Id { get; }
        public int DataId { get; }

        public Type DataType { get; }
        public ItemType ItemType { get; }

        public LocalizeKey NameKey { get; }
        public AssetReference IconRef { get; }
        public ItemBoundsInfo Bounds { get; }

        public IReadOnlyList<ItemsGridCategory> GridCategories { get; private set; }
        public IReadOnlyList<ItemsGridCellType> GridCellTypes { get; private set; }

        public ItemSlot Slot { get; }
        public string SlotId { get; private set; }
        public bool IsPlacedInSlot { get; private set; }

        public ReadOnlyReactiveProperty<bool> IsRemoved => isRemoved;

        protected ItemInfo(string id, ItemData data)
        {
            Id = id;
            DataId = data.Id;
            DataType = data.GetType();
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
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ItemInfo);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(ItemInfo a, ItemInfo b)
        {
            return EqualityComparer<ItemInfo>.Default.Equals(a, b);
        }

        public static bool operator !=(ItemInfo a, ItemInfo b)
        {
            return !(a == b);
        }
    }
}