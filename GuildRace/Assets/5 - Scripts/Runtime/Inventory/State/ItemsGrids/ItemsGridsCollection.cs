using System.Collections.Generic;
using AD.ToolsCollection;

namespace AD.Game.Inventory
{
    public class ItemsGridsCollection : ReactiveCollection<ItemsGridInfo>, IItemsGridsCollection
    {
        public ItemsGridsCollection(IEnumerable<ItemsGridInfo> values) : base(values)
        {
        }
    }
}