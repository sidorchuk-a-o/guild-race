using AD.States;
using System.Collections.Generic;

namespace Game.Inventory
{
    public class ItemsGridsCollection : ReactiveCollectionInfo<ItemsGridInfo>, IItemsGridsCollection
    {
        public ItemsGridsCollection(IEnumerable<ItemsGridInfo> values) : base(values)
        {
        }
    }
}