using System;
using System.Collections.Generic;
using System.Linq;
using Game.Inventory;
using UnityEngine;

namespace Game.Instances
{
    public class UnitBagInfo : ItemsGridInfo
    {
        public UnitBagInfo(string id, UnitBagData data, IEnumerable<ItemInfo> items = null)
            : base(id, data, items)
        {
        }

        public override bool CheckPossibilityOfPlacement(ItemInfo item, in Vector3Int positionOnGrid)
        {
            if (AlreadyContainsType(item as ConsumablesItemInfo))
            {
                return false;
            }

            return base.CheckPossibilityOfPlacement(item, positionOnGrid);
        }

        public override bool TryPlaceItem(ItemInfo item, in Vector3Int positionOnGrid)
        {
            if (AlreadyContainsType(item as ConsumablesItemInfo))
            {
                return false;
            }

            return base.TryPlaceItem(item, positionOnGrid);
        }

        // == IPlacementContainer ==

        public override bool CheckPossibilityOfPlacement(ItemInfo item)
        {
            if (AlreadyContainsType(item as ConsumablesItemInfo))
            {
                return false;
            }

            return base.CheckPossibilityOfPlacement(item);
        }

        public override bool TryPlaceItem(ItemInfo item)
        {
            if (AlreadyContainsType(item as ConsumablesItemInfo))
            {
                return false;
            }

            return base.TryPlaceItem(item);
        }

        // == Common ==

        private bool AlreadyContainsType(ConsumablesItemInfo item)
        {
            var consumables = Items
                .OfType<ConsumablesItemInfo>()
                .Select(x => x.Type);

            return consumables.Contains(item.Type);
        }
    }
}