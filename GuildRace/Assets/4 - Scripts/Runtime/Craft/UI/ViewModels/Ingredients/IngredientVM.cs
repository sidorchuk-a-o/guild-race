using AD.Services.Router;
using Game.Inventory;
using UniRx;

namespace Game.Craft
{
    public class IngredientVM : ViewModel
    {
        private readonly ReactiveProperty<int> craftCount = new();
        private readonly ReactiveProperty<bool> isAvailable = new();

        public int Id { get; }

        public int Count { get; }
        public ReagentDataVM ReagentVM { get; }
        public ItemCounterVM ReagentCounterVM { get; }

        public IReadOnlyReactiveProperty<int> CraftCount => craftCount;
        public IReadOnlyReactiveProperty<bool> IsAvailable => isAvailable;

        public IngredientVM(IngredientData data, CraftVMFactory craftVMF)
        {
            Id = data.ReagentId;
            Count = data.Count;

            ReagentVM = craftVMF.InventoryVMF.CreateItemData(Id) as ReagentDataVM;
            ReagentCounterVM = craftVMF.GetReagentItemCounter(ReagentVM.Id);
        }

        protected override void InitSubscribes()
        {
            ReagentVM.AddTo(this);
            ReagentCounterVM.AddTo(this);
        }

        public void SetCraftingCount(int value)
        {
            craftCount.Value = Count * value;
            isAvailable.Value = ReagentCounterVM.Count.Value >= craftCount.Value;
        }
    }
}