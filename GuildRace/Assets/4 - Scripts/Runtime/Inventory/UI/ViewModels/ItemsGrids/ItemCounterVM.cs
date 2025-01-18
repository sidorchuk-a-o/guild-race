using AD.Services.Router;
using UniRx;

namespace Game.Inventory
{
    public class ItemCounterVM : ViewModel
    {
        private readonly ItemCounter counter;

        public ItemDataVM ItemDataVM { get; } 
        public IReadOnlyReactiveProperty<int> Count => counter.Count;

        public ItemCounterVM(ItemCounter counter, InventoryVMFactory inventoryVMF)
        {
            this.counter = counter;

            ItemDataVM = inventoryVMF.CreateItemData(counter.ItemDataId);
        }

        protected override void InitSubscribes()
        {
            counter.AddTo(this);
            ItemDataVM.AddTo(this);
        }
    }
}