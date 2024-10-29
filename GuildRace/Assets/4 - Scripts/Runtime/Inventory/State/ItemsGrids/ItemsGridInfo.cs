using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventory
{
    public class ItemsGridInfo : IPlacementContainer
    {
        private readonly ItemsCollection items;

        public string Id { get; }
        public string DataId { get; }

        public Vector3Int Size { get; }
        public ItemsGridCategory Category { get; }
        public ItemsGridCellType CellType { get; }

        public IItemsCollection Items => items;

        public ItemsGridInfo(string id, ItemsGridData data, IEnumerable<ItemInfo> items = null)
        {
            this.items = new(items);

            Id = id;
            DataId = data.Id;
            Size = new(data.RowSize, data.RowCount);
            Category = data.Category;
            CellType = data.CellType;
        }

        public bool CheckPossibilityOfPlacement(ItemInfo item, in Vector3Int positionOnGrid)
        {
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

        public bool TryPlaceItem(ItemInfo item, in Vector3Int positionOnGrid)
        {
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

        public bool CheckPossibilityOfPlacement(ItemInfo item)
        {
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
            var normalizedWidth = Size.x - item.Bounds.Size.x;
            var normalizedHeight = Size.y - item.Bounds.Size.y;

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

        public bool TryPlaceItem(ItemInfo item)
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
            var normalizedWidth = Size.x - item.Bounds.Size.x;
            var normalizedHeight = Size.y - item.Bounds.Size.y;

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

        public bool TryRemoveItem(ItemInfo item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                return true;
            }

            return false;
        }

        // == Utils ==

        private bool CheckItemBounds(in BoundsInt itemBounds)
        {
            return itemBounds.min.x >= 0
                && itemBounds.min.y >= 0
                && itemBounds.max.x <= Size.x
                && itemBounds.max.y <= Size.y
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