using AD.States;
using System.Collections.Generic;

namespace Game.Inventory
{
    public class ItemsCollection : ReactiveCollectionInfo<ItemInfo>, IItemsCollection
    {
        public ItemsCollection(IEnumerable<ItemInfo> values) : base(values)
        {
        }
    }
}