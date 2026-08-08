using AD.Game.Inventory;
using AD.Router;
using R3;

namespace AD.Game.Craft
{
    public class IngredientVM : ViewModel
    {
        private readonly ReactiveProperty<int> craftCount = new();
        private readonly ReactiveProperty<bool> isAvailable = new();

        public int Id { get; }

        public int Count { get; }
        public ReagentDataVM ReagentVM { get; }
        public ItemCounterVM ReagentCounterVM { get; }

        public ReadOnlyReactiveProperty<int> CraftCount => craftCount;
        public ReadOnlyReactiveProperty<bool> IsAvailable => isAvailable;

        public IngredientVM(IngredientData data, CraftVMFactory craftVMF)
        {
            Id = data.ReagentId;
            Count = data.Count;

            ReagentVM = (ReagentDataVM)craftVMF.InventoryVMF.CreateItemData(Id);
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
            isAvailable.Value = ReagentCounterVM.Count.CurrentValue >= craftCount.Value;
        }
    }
}