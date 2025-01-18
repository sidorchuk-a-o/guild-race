using AD.ToolsCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Game.Inventory
{
    public class ItemCounter : IDisposable
    {
        private readonly ReactiveProperty<int> count = new();

        private readonly CompositeDisp disp = new();
        private readonly Dictionary<string, CompositeDisp> disps = new();

        public int ItemDataId { get; }
        public IReadOnlyReactiveProperty<int> Count => count;

        public ItemCounter(int itemDataId, IEnumerable<ItemsGridInfo> itemsGrids)
        {
            foreach (var itemsGrid in itemsGrids)
            {
                itemsGrid.Items
                    .ObserveAdd()
                    .Where(x => x.Value.DataId == itemDataId)
                    .Select(x => x.Value)
                    .Subscribe(AddItemCallback)
                    .AddTo(disp);

                itemsGrid.Items
                    .ObserveRemove()
                    .Where(x => x.Value.DataId == itemDataId)
                    .Select(x => x.Value)
                    .Subscribe(RemoveItemCallback)
                    .AddTo(disp);

                var filteredItems = itemsGrid.Items
                    .Where(x => x.DataId == itemDataId);

                foreach (var item in filteredItems)
                {
                    AddItemCallback(item);
                }
            }

            ItemDataId = itemDataId;
        }

        private void AddItemCallback(ItemInfo item)
        {
            var count = GetCount(item);

            AddCount(count);

            if (item is IStackableItem stackableItem)
            {
                var itemDisp = new CompositeDisp();

                stackableItem.Stack
                    .ObserveChanged()
                    .Select(x => x.Delta)
                    .Subscribe(AddCount)
                    .AddTo(itemDisp);

                disps[item.Id] = itemDisp;
            }
        }

        private void RemoveItemCallback(ItemInfo item)
        {
            var count = GetCount(item);

            AddCount(-count);

            if (disps.Remove(item.Id, out var itemDisp))
            {
                itemDisp.Clear();
            }
        }

        private static int GetCount(ItemInfo item)
        {
            const int defaultCount = 1;

            return item switch
            {
                IStackableItem sItem => sItem.Stack.Value,
                _ => defaultCount
            };
        }

        private void AddCount(int value)
        {
            count.Value = value;
        }

        void IDisposable.Dispose()
        {
            disp.Dispose();
            disps.ForEach(x => x.Value.Dispose());
        }
    }
}