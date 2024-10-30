using AD.Services.Router;
using AD.ToolsCollection;
using UniRx;

namespace Game.Inventory
{
    public abstract class ItemSlotVM : ViewModel
    {
        private readonly ItemSlotInfo info;
        private readonly InventoryVMFactory inventoryVMF;

        private readonly ReactiveProperty<ItemVM> itemVM = new();

        public string Id { get; }
        public ItemSlot Slot { get; }

        public bool HasItem { get; private set; }
        public IReadOnlyReactiveProperty<ItemVM> ItemVM => itemVM;

        public UIStateVM PickupStateVM { get; }

        public ItemSlotVM(ItemSlotInfo info, InventoryVMFactory inventoryVMF)
        {
            this.info = info;
            this.inventoryVMF = inventoryVMF;

            Id = info.Id;
            Slot = info.Slot;
            PickupStateVM = new();
        }

        protected override void InitSubscribes()
        {
            PickupStateVM.AddTo(this);

            info.Item
                .Subscribe(ItemChangedCallback)
                .AddTo(this);
        }

        private void ItemChangedCallback(ItemInfo info)
        {
            if (itemVM.IsValid())
            {
                itemVM.Value.ResetSubscribes();
            }

            if (info != null)
            {
                var vm = inventoryVMF.CreateItem(info);

                vm.AddTo(this);

                itemVM.Value = vm;
            }
            else
            {
                itemVM.Value = null;
            }

            HasItem = itemVM.IsValid();
        }

        // == Slot ==

        public bool CheckPossibilityOfPlacement(ItemVM itemVM)
        {
            return inventoryVMF.CheckPossibilityOfPlacement(new PlaceInSlotArgs
            {
                ItemId = itemVM.Id,
                SlotId = info.Id
            });
        }

        public bool TryAddItem(ItemVM itemVM)
        {
            return inventoryVMF.TryEquipItem(new PlaceInSlotArgs
            {
                ItemId = itemVM.Id,
                SlotId = info.Id
            });
        }

        public bool TryRemoveItem()
        {
            if (!HasItem)
            {
                return false;
            }

            return inventoryVMF.TryRemoveItem(new RemoveFromSlotArgs
            {
                ItemId = itemVM.Value.Id
            });
        }
    }
}