using System.Collections.Generic;
using System.Linq;
using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Inventory
{
    public class ItemCounterVM : ViewModel
    {
        private readonly int itemDataId;
        private readonly ItemsGridInfo[] itemsGrids;
        private readonly ReactiveProperty<int> count = new();

        public ItemDataVM ItemDataVM { get; }
        public IReadOnlyReactiveProperty<int> Count => count;

        public ItemCounterVM(
            int itemDataId,
            IEnumerable<ItemsGridInfo> itemsGrids,
            InventoryVMFactory inventoryVMF)
        {
            this.itemDataId = itemDataId;
            this.itemsGrids = itemsGrids.ToArray();

            ItemDataVM = inventoryVMF.CreateItemData(itemDataId);
        }

        protected override void InitSubscribes()
        {
            ItemDataVM.AddTo(this);

            InitGridsSubscribes();
        }

        private void InitGridsSubscribes()
        {
            count.Value = 0;

            foreach (var itemsGrid in itemsGrids)
            {
                itemsGrid.Items
                    .ObserveAdd()
                    .Where(x => x.Value.DataId == itemDataId)
                    .Select(x => x.Value)
                    .Subscribe(AddItemCallback)
                    .AddTo(this);

                itemsGrid.Items
                    .ObserveRemove()
                    .Where(x => x.Value.DataId == itemDataId)
                    .Select(x => x.Value)
                    .Subscribe(RemoveItemCallback)
                    .AddTo(this);

                var filteredItems = itemsGrid.Items
                    .Where(x => x.DataId == itemDataId);

                foreach (var item in filteredItems)
                {
                    AddItemCallback(item);
                }
            }
        }

        private void AddItemCallback(ItemInfo item)
        {
            var count = GetCount(item);

            AddCount(count);

            if (item is IStackableItem stackableItem)
            {
                stackableItem.Stack
                    .ObserveChanged()
                    .Select(x => x.Delta)
                    .Subscribe(AddCount)
                    .AddTo(this);
            }
        }

        private void RemoveItemCallback(ItemInfo item)
        {
            var count = GetCount(item);

            AddCount(-count);
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
            count.Value += value;
        }
    }
}