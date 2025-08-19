using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Game.Inventory
{
    public abstract class ItemsGridInfo : IPlacementContainer
    {
        private readonly ItemsCollection items;
        private readonly ReactiveProperty<Vector3Int> size = new();

        public string Id { get; }
        public int DataId { get; }

        public ItemsGridCategory Category { get; }
        public ItemsGridCellType CellType { get; }

        public int DefaultRowsCount { get; }
        public int DefaultColumnsCount { get; }
        public int RowsCount => size.Value.y;
        public int ColumnsCount => size.Value.x;
        public IReadOnlyReactiveProperty<Vector3Int> Size => size;

        public IItemsCollection Items => items;

        public ItemsGridInfo(string id, ItemsGridData data, IEnumerable<ItemInfo> items = null)
        {
            this.items = new(items);

            Id = id;
            DataId = data.Id;
            Category = data.Category;
            CellType = data.CellType;

            DefaultRowsCount = data.RowCount;
            DefaultColumnsCount = data.RowSize;

            size.Value = new(data.RowSize, data.RowCount);
        }

        public void SetSize(int rows, int columns)
        {
            size.Value = new(columns, rows);
        }

        public virtual bool CheckPossibilityOfPlacement(ItemInfo item, in Vector3Int positionOnGrid)
        {
            if (!item.CheckGridParams(this))
            {
                return false;
            }

            var itemBounds = new BoundsInt(positionOnGrid, item.Bounds.Size);

            if (!CheckItemBounds(itemBounds))
            {
                return false;
            }

            if (!NotIntersects(itemBounds))
            {
                return false;
            }

            return true;
        }

        public virtual bool TryPlaceItem(ItemInfo item, in Vector3Int positionOnGrid)
        {
            if (!item.CheckGridParams(this))
            {
                return false;
            }

            var itemBounds = new BoundsInt(positionOnGrid, item.Bounds.Size);

            if (!CheckItemBounds(itemBounds))
            {
                return false;
            }

            return PlaceItem(item, positionOnGrid);
        }

        // == IPlacementContainer ==

        public IEnumerable<ItemInfo> GetItems()
        {
            return items;
        }

        public bool CheckBasePlacementParams(ItemInfo item)
        {
            return item.CheckGridParams(this);
        }

        public virtual bool CheckPossibilityOfPlacement(ItemInfo item)
        {
            if (!item.CheckGridParams(this))
            {
                return false;
            }

            var originPosition = item.Bounds.Position;
            var result = false;

            if (FindPlacement(item))
            {
                result = true;
            }
            else
            {
                item.Bounds.Rotate();

                if (FindPlacement(item))
                {
                    result = true;
                }

                item.Bounds.Rotate();
            }

            item.Bounds.SetPosition(originPosition);

            return result;
        }

        private bool FindPlacement(ItemInfo item)
        {
            var normalizedWidth = Size.Value.x - item.Bounds.Size.x;
            var normalizedHeight = Size.Value.y - item.Bounds.Size.y;

            for (var y = 0; y <= normalizedHeight; y++)
            {
                for (var x = 0; x <= normalizedWidth; x++)
                {
                    var positionOnGrid = new Vector3Int(x, y);

                    item.Bounds.SetPosition(positionOnGrid);

                    if (NotIntersects(item))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public virtual bool TryPlaceItem(ItemInfo item)
        {
            if (PlaceItem(item))
            {
                return true;
            }
            else
            {
                item.Bounds.Rotate();

                if (PlaceItem(item))
                {
                    return true;
                }

                item.Bounds.Rotate();
            }

            return false;
        }

        private bool PlaceItem(ItemInfo item)
        {
            var normalizedWidth = Size.Value.x - item.Bounds.Size.x;
            var normalizedHeight = Size.Value.y - item.Bounds.Size.y;

            for (var y = 0; y <= normalizedHeight; y++)
            {
                for (var x = 0; x <= normalizedWidth; x++)
                {
                    var positionOnGrid = new Vector3Int(x, y);

                    if (PlaceItem(item, positionOnGrid))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool PlaceItem(ItemInfo item, in Vector3Int positionOnGrid)
        {
            item.Bounds.SetPosition(positionOnGrid);

            if (!NotIntersects(item))
            {
                return false;
            }

            items.Add(item);

            return true;
        }

        public virtual bool TryRemoveItem(ItemInfo item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                return true;
            }

            return false;
        }

        // == Common ==

        private bool CheckItemBounds(in BoundsInt itemBounds)
        {
            return itemBounds.min.x >= 0
                && itemBounds.min.y >= 0
                && itemBounds.max.x <= Size.Value.x
                && itemBounds.max.y <= Size.Value.y
                && NotIntersects(itemBounds);
        }

        private bool NotIntersects(ItemInfo item)
        {
            return NotIntersects(item.Bounds.Value);
        }

        private bool NotIntersects(in BoundsInt itemBounds)
        {
            foreach (var item in items)
            {
                if (itemBounds.Intersects2D(item.Bounds.Value))
                {
                    return false;
                }
            }

            return true;
        }
    }
}