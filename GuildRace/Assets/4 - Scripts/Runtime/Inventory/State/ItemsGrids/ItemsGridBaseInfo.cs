using System.Collections.Generic;

namespace Game.Inventory
{
    public class ItemsGridBaseInfo : ItemsGridInfo
    {
        public ItemsGridBaseInfo(string id, ItemsGridBaseData data, IEnumerable<ItemInfo> items = null)
            : base(id, data, items)
        {
        }
    }
}