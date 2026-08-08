using System.Collections.Generic;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemsCollection : ReactiveCollection<ItemInfo>, IItemsCollection
    {
        public ItemsCollection(IEnumerable<ItemInfo> values) : base(values)
        {
        }
    }
}