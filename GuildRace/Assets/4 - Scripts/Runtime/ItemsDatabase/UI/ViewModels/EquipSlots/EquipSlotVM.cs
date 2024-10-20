using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Items
{
    public class EquipSlotVM : VMBase
    {
        private readonly EquipSlotInfo info;
        private readonly ItemsVMFactory itemsVMF;

        private readonly ReactiveProperty<EquipItemVM> itemVM = new();

        public EquipSlot Slot { get; }

        public bool HasItem { get; private set; }
        public IReadOnlyReactiveProperty<EquipItemVM> ItemVM => itemVM;

        public EquipSlotVM(EquipSlotInfo info, ItemsVMFactory itemsVMF)
        {
            this.info = info;
            this.itemsVMF = itemsVMF;

            Slot = info.Slot;
        }

        protected override void InitSubscribes(CompositeDisp disp)
        {
            info.Item
                .Subscribe(x => ItemChangedCallback(x, disp))
                .AddTo(disp);
        }

        private void ItemChangedCallback(EquipItemInfo info, CompositeDisp disp)
        {
            if (itemVM.IsValid())
            {
                itemVM.Value.ResetSubscribes();
            }

            if (info != null)
            {
                var vm = itemsVMF.CreateEquipItem(info);

                vm.AddTo(disp);

                itemVM.Value = vm;
            }
            else
            {
                itemVM.Value = null;
            }

            HasItem = itemVM.IsValid();
        }
    }
}